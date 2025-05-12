using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using System.Linq;

namespace fl_api.Services
{
    public class ForecastService : IForecastService
    {
        public Task<List<ForecastPointDto>> ForecastAsync(
            IEnumerable<DailyDemandDto> history,
            string horizon)
        {
            // Función para agrupar en el primer día del mes o semestre
            Func<DateTime, DateTime> periodKey = horizon.ToLower() switch
            {
                "monthly" => d => new DateTime(d.Year, d.Month, 1),
                "semestral" => d =>
                {
                    var half = (d.Month - 1) / 6;
                    return new DateTime(d.Year, half * 6 + 1, 1);
                }
                ,
                _ => throw new ArgumentException("Horizon debe ser 'monthly' o 'semestral'")
            };

            // 1) Agrupamos y ordenamos
            var series = history
                .GroupBy(d => periodKey(d.Date))
                .OrderBy(g => g.Key)
                .Select((g, idx) => new { Index = idx, Period = g.Key, Total = g.Sum(x => x.Quantity) })
                .ToList();

            // 2) Si no hay nada, no hay forecast
            if (series.Count == 0)
                return Task.FromResult(new List<ForecastPointDto>());

            // 3) Determinamos cuántos pasos predecir
            int h = horizon.ToLower() == "monthly" ? 6 : 2;

            // 4) Caso especial: sólo un punto → repetimos ese valor
            if (series.Count == 1)
            {
                var single = series[0];
                var results = new List<ForecastPointDto>();
                for (int i = 1; i <= h; i++)
                {
                    DateTime next = horizon.ToLower() switch
                    {
                        "monthly" => single.Period.AddMonths(i),
                        "semestral" => single.Period.AddMonths(i * 6),
                        _ => single.Period
                    };
                    results.Add(new ForecastPointDto
                    {
                        PeriodStart = next,
                        ForecastedQuantity = single.Total
                    });
                }
                return Task.FromResult(results);
            }

            // 5) Regresión lineal si hay 2 o más puntos
            int n = series.Count;
            double sumX = series.Sum(p => p.Index);
            double sumY = series.Sum(p => p.Total);
            double sumXY = series.Sum(p => p.Index * p.Total);
            double sumX2 = series.Sum(p => p.Index * p.Index);

            double denom = n * sumX2 - sumX * sumX;
            double b = denom == 0 ? 0 : (n * sumXY - sumX * sumY) / denom;
            double a = (sumY - b * sumX) / n;

            var lastPeriod = series.Last().Period;
            var forecast = new List<ForecastPointDto>();
            for (int i = 1; i <= h; i++)
            {
                DateTime next = horizon.ToLower() switch
                {
                    "monthly" => lastPeriod.AddMonths(i),
                    "semestral" => lastPeriod.AddMonths(i * 6),
                    _ => lastPeriod
                };
                double xNext = series.Count - 1 + i;
                double yNext = a + b * xNext;
                forecast.Add(new ForecastPointDto
                {
                    PeriodStart = next,
                    ForecastedQuantity = Math.Max(0, yNext)
                });
            }
            return Task.FromResult(forecast);
        }
    }
}
