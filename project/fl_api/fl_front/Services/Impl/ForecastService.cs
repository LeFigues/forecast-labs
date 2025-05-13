using fl_front.Dtos;
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

        public async Task<List<ForecastHistoricoDto>> GetForecastHistoricoAsync()
        {
            return await _http.GetFromJsonAsync<List<ForecastHistoricoDto>>("api/forecast/insumos-historico") ?? new();
        }
        public async Task<List<ForecastPracticaDto>> GetForecastPracticasUsoAsync()
        {
            return await _http.GetFromJsonAsync<List<ForecastPracticaDto>>("api/forecast/practicas-uso") ?? new();
        }
        public async Task<List<ForecastRiesgoDto>> GetForecastRiesgoAsync()
        {
            return await _http.GetFromJsonAsync<List<ForecastRiesgoDto>>("api/forecast/insumos-riesgo") ?? new();
        }

    }
}
