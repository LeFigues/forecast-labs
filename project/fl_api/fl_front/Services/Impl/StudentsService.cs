using fl_front.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace fl_front.Services.Impl
{

    public class StudentsService : IStudentsService
    {
        private readonly HttpClient _http;

        public StudentsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Career>> GetCareersAsync()
        {
            try
            {
                var raw = await _http.GetFromJsonAsync<JsonObject>("api/careers");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var values = raw?["$values"]?.Deserialize<List<Career>>(options);
                return values ?? new List<Career>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener carreras: {ex.Message}");
                return new List<Career>();
            }
        }

    }
}
