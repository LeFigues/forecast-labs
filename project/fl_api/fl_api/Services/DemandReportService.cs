using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Services
{
    public class DemandReportService : IDemandReportService
    {
        private readonly IMongoDbService _mongo;

        public DemandReportService(IMongoDbService mongo)
            => _mongo = mongo;

        public async Task<List<DemandReportDetailDto>> GetDemandReportDetailAsync(
            DateTime fromDate, DateTime toDate)
        {
            // Normaliza día completo
            var start = fromDate.Date;
            var end = toDate.Date.AddDays(1);

            // 1) Leer clasificaciones
            var clsCol = _mongo.GetCollection<ClassificationRecord>("classifications");
            var classifications = await clsCol
                .Find(c => c.ClassifiedAt >= start && c.ClassifiedAt < end)
                .ToListAsync();
            if (!classifications.Any())
                return new List<DemandReportDetailDto>();

            // 2) Leer análisis correspondientes
            var openaiCol = _mongo.GetCollection<PythonAnalysisRecord>("openai");
            var docIds = classifications.Select(c => c.DocumentId).Distinct().ToList();
            var analyses = await openaiCol
                .Find(a => docIds.Contains(a.DocumentId))
                .ToListAsync();

            // 3) Indexar clasificación por DocumentId
            var clsLookup = classifications
                .ToDictionary(c => c.DocumentId, c => c);

            // 4) Acumular en memoria
            var report = new Dictionary<
                (int careerId, string careerName, int subjectId, string subjectName),
                (Dictionary<string, (string unit, int qty)> eq,
                  Dictionary<string, (string unit, int qty)> sup,
                  Dictionary<string, (string unit, int qty)> rea
                )
            >();

            foreach (var analysis in analyses)
            {
                if (!clsLookup.TryGetValue(analysis.DocumentId, out var cls))
                    continue;

                var key = (cls.CareerId, cls.CareerName, cls.SubjectId, cls.SubjectName);
                if (!report.ContainsKey(key))
                    report[key] = (new(), new(), new());

                var (eqAcc, supAcc, reaAcc) = report[key];
                int groups = analysis.Groups;

                // equipment
                foreach (var item in analysis.AnalysisResult["materials"]["equipment"].AsBsonArray)
                {
                    var doc = item.AsBsonDocument;
                    var desc = doc["description"].AsString;
                    var unit = doc["unit"].AsString;
                    var qty = doc["quantity_per_group"].AsInt32 * groups;
                    if (eqAcc.ContainsKey(desc)) eqAcc[desc] = (unit, eqAcc[desc].qty + qty);
                    else eqAcc[desc] = (unit, qty);
                }

                // supplies
                foreach (var item in analysis.AnalysisResult["materials"]["supplies"].AsBsonArray)
                {
                    var doc = item.AsBsonDocument;
                    var desc = doc["description"].AsString;
                    var unit = doc["unit"].AsString;
                    var qty = doc["quantity_per_group"].AsInt32 * groups;
                    if (supAcc.ContainsKey(desc)) supAcc[desc] = (unit, supAcc[desc].qty + qty);
                    else supAcc[desc] = (unit, qty);
                }

                // reactives
                foreach (var item in analysis.AnalysisResult["materials"]["reactives"].AsBsonArray)
                {
                    var doc = item.AsBsonDocument;
                    var desc = doc["description"].AsString;
                    var unit = doc["unit"].AsString;
                    var qty = doc["quantity_per_group"].AsInt32 * groups;
                    if (reaAcc.ContainsKey(desc)) reaAcc[desc] = (unit, reaAcc[desc].qty + qty);
                    else reaAcc[desc] = (unit, qty);
                }
            }

            // 5) Construir DTOs
            var result = report.Select(kvp =>
            {
                var meta = kvp.Key;
                var (eqAcc, supAcc, reaAcc) = kvp.Value;

                return new DemandReportDetailDto
                {
                    CareerId = meta.careerId,
                    CareerName = meta.careerName,
                    SubjectId = meta.subjectId,
                    SubjectName = meta.subjectName,
                    Equipment = eqAcc.Select(e => new ItemDto
                    {
                        Description = e.Key,
                        Unit = e.Value.unit,
                        TotalQuantity = e.Value.qty
                    }).ToList(),
                    Supplies = supAcc.Select(s => new ItemDto
                    {
                        Description = s.Key,
                        Unit = s.Value.unit,
                        TotalQuantity = s.Value.qty
                    }).ToList(),
                    Reactives = reaAcc.Select(r => new ItemDto
                    {
                        Description = r.Key,
                        Unit = r.Value.unit,
                        TotalQuantity = r.Value.qty
                    }).ToList()
                };
            })
            .ToList();

            return result;
        }

        public async Task<DemandSummaryDto> GetDemandSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            // Reusa el detalle que ya implementaste
            var details = await GetDemandReportDetailAsync(fromDate, toDate);

            return new DemandSummaryDto
            {
                TotalEquipmentItems = details
                    .SelectMany(d => d.Equipment)
                    .Sum(i => i.TotalQuantity),

                TotalSupplyItems = details
                    .SelectMany(d => d.Supplies)
                    .Sum(i => i.TotalQuantity),

                TotalReactiveItems = details
                    .SelectMany(d => d.Reactives)
                    .Sum(i => i.TotalQuantity)
            };
        }


        public async Task<List<DailyDemandDto>> GetDemandHistoryAsync(DateTime fromDate, DateTime toDate)
        {
            // 1) Ajustamos el rango para cubrir TODO el día:
            var startOfDay = fromDate.Date.ToUniversalTime();
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

            // 2) Traemos las clasificaciones en ese rango
            var clsColl = _mongo.GetCollection<ClassificationRecord>("classifications");
            var classifications = await clsColl
                .Find(c =>
                    c.ClassifiedAt >= startOfDay &&
                    c.ClassifiedAt <= endOfDay
                )
                .ToListAsync();

            // Si no hay nada, devolvemos un array vacío junto con un log
            if (!classifications.Any())
            {
                // Aquí podrías loguear classifications.Count en tu logger
                return new List<DailyDemandDto>();
            }

            // 3) Obtenemos todos los análisis de una vez
            var docIds = classifications.Select(c => c.DocumentId).Distinct().ToList();
            var openaiColl = _mongo.GetCollection<PythonAnalysisRecord>("openai");
            var analyses = await openaiColl
                .Find(a => docIds.Contains(a.DocumentId))
                .ToListAsync();

            var temp = new List<DailyDemandDto>();

            // 4) Flattening en memoria
            foreach (var cls in classifications)
            {
                var analysis = analyses.FirstOrDefault(a => a.DocumentId == cls.DocumentId);
                if (analysis == null)
                    continue;

                var date = cls.ClassifiedAt; // o analysis.AnalyzedAt si prefieres

                // equipment
                foreach (var eq in analysis.AnalysisResult["materials"]["equipment"].AsBsonArray)
                {
                    temp.Add(new DailyDemandDto
                    {
                        Date = date,
                        Item = eq["description"].AsString,
                        Unit = eq["unit"].AsString,
                        Quantity = eq["quantity_per_group"].AsInt32
                    });
                }
                // supplies
                foreach (var sup in analysis.AnalysisResult["materials"]["supplies"].AsBsonArray)
                {
                    temp.Add(new DailyDemandDto
                    {
                        Date = date,
                        Item = sup["description"].AsString,
                        Unit = sup["unit"].AsString,
                        Quantity = sup["quantity_per_group"].AsInt32
                    });
                }
                // reactives
                if (analysis.AnalysisResult["materials"].AsBsonDocument
                       .TryGetValue("reactives", out var rxArr)
                    && rxArr.IsBsonArray)
                {
                    foreach (var rx in rxArr.AsBsonArray)
                    {
                        temp.Add(new DailyDemandDto
                        {
                            Date = date,
                            Item = rx["description"].AsString,
                            Unit = rx["unit"].AsString,
                            Quantity = rx["quantity_per_group"].AsInt32
                        });
                    }
                }
            }

            // 5) Agrupamos por día/item/unidad
            var grouped = temp
                .GroupBy(x => new { x.Date.Date, x.Item, x.Unit })
                .Select(g => new DailyDemandDto
                {
                    Date = g.Key.Date,
                    Item = g.Key.Item,
                    Unit = g.Key.Unit,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Item)
                .ToList();

            return grouped;
        }



    }
}
