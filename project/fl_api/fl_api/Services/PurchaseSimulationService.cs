using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;

namespace fl_api.Services
{
    public class PurchaseSimulationService : IPurchaseSimulationService
    {
        private readonly IDemandReportService _demandService;
        private readonly PurchaseSimulationSettings _settings;

        public PurchaseSimulationService(
            IDemandReportService demandService,
            IOptions<PurchaseSimulationSettings> opts)
        {
            _demandService = demandService;
            _settings = opts.Value;
        }

        public async Task<PurchaseSimulationDto> SimulateAsync(DateTime from, DateTime to)
        {
            // 1) Obtenemos el detalle de demanda
            var details = await _demandService.GetDemandReportDetailAsync(from, to);

            // 2) Cálculo de días en el rango
            var days = (to.Date - from.Date).TotalDays + 1;

            // 3) Aplanamos todos los ítems (equip, sup, reactivos)
            var allItems =
                details.SelectMany(d => d.Equipment)
                       .Concat(details.SelectMany(d => d.Supplies))
                       .Concat(details.SelectMany(d => d.Reactives))
                       .ToList();

            // 4) Agrupamos por descripción+unidad y sumamos demanda
            var grouped = allItems
                .GroupBy(i => (i.Description, i.Unit))
                .Select(g =>
                {
                    var total = g.Sum(x => x.TotalQuantity);
                    var avgDaily = days > 0
                        ? total / days
                        : 0;
                    var recommended = (int)Math.Ceiling(avgDaily * _settings.CoverageDays);
                    return new PurchaseItemDto
                    {
                        Description = g.Key.Description,
                        Unit = g.Key.Unit,
                        TotalDemand = total,
                        AverageDailyUse = Math.Round(avgDaily, 2),
                        RecommendedOrder = recommended
                    };
                })
                .ToList();

            // 5) Montamos el DTO de salida
            return new PurchaseSimulationDto
            {
                From = from,
                To = to,
                CoverageDays = _settings.CoverageDays,
                LeadTimeDays = _settings.LeadTimeDays,
                Items = grouped
            };
        }
    }
}
