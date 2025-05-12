using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace fl_api.Services
{
    public class StudentsApiClient : IStudentsApiClient
    {
        private readonly HttpClient _http;

        public StudentsApiClient(HttpClient http, IOptions<ApiStudentsSettings> options)
        {
            _http = http;
            _http.BaseAddress = new(options.Value.BaseUrl);
        }

        public Task<HttpResponseMessage> PingAsync()
        {
            // Asegúrate de que fl_students expone GET /api/health
            return _http.GetAsync("/api/health");
        }
        public async Task<List<CareerDto>> GetCareersAsync()
        {
            var res = await _http.GetAsync("/api/careers");
            res.EnsureSuccessStatusCode();
            using var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());

            // El root contiene "$values": [ { … CareerDto … }, … ]
            var values = doc.RootElement.GetProperty("$values");
            var list = new List<CareerDto>();
            foreach (var elt in values.EnumerateArray())
            {
                // Deserializamos cada elemento a CareerDto
                var career = JsonSerializer.Deserialize<CareerDto>(
                    elt.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                )!;
                list.Add(career);
            }
            return list;
        }
    }
}
