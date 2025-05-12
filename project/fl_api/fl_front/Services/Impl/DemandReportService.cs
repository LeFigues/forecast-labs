using fl_front.Models;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class DemandReportService : IDemandReportService
    {
        private readonly HttpClient _http;

        public DemandReportService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DemandSummary?> GetSummaryAsync(DateTime from, DateTime to)
        {
            return await _http.GetFromJsonAsync<DemandSummary>($"api/reports/demand-summary?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
        }

        public async Task<List<DemandDetail>> GetDetailAsync(DateTime from, DateTime to)
        {
            return await _http.GetFromJsonAsync<List<DemandDetail>>($"api/reports/demand-detail?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}")
                   ?? new();
        }

        public async Task<DemandSimulation?> GetSimulationAsync(DateTime from, DateTime to)
        {
            return await _http.GetFromJsonAsync<DemandSimulation>($"api/reports/demand-simulation?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
        }

        public async Task<List<DemandHistoryItem>> GetHistoryAsync(DateTime from, DateTime to)
        {
            return await _http.GetFromJsonAsync<List<DemandHistoryItem>>($"api/reports/demand-history?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}")
                   ?? new();
        }
    }
}
