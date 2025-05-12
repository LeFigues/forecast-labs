using fl_front.Models;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient _http;

        public ForecastService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ForecastResult>> GetForecastAsync(ForecastRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/forecast", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ForecastResult>>() ?? new();
        }
    }
}
