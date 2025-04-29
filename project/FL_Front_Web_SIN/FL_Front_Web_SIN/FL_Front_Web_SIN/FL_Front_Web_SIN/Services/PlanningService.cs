
// Services/PlanningService.cs
using FL_Front_Web_SIN.Models;
using Newtonsoft.Json.Linq;

namespace FL_Front_Web_SIN.Services
{
    public class PlanningService
    {
        private readonly HttpClient _httpClient;

        public PlanningService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("StudentsAPI");
        }

        public async Task<List<Career>> GetCareersAsync()
        {
            var response = await _httpClient.GetStringAsync("Careers");
            var json = JObject.Parse(response);
            var values = json["$values"];
            return values?.ToObject<List<Career>>() ?? new();
        }

        public async Task<List<SGroup>> GetGroupsBySubjectAsync(int subjectId)
        {
            var response = await _httpClient.GetStringAsync($"SGroups/by-subject/{subjectId}");
            var json = JObject.Parse(response);
            var values = json["$values"];
            return values?.ToObject<List<SGroup>>() ?? new();
        }

        public async Task<List<Student>> GetStudentsByGroupAsync(int groupId)
        {
            var response = await _httpClient.GetStringAsync($"SGroups/{groupId}");
            var json = JObject.Parse(response);
            var values = json["students"]?["$values"];
            return values?.ToObject<List<Student>>() ?? new();
        }
    }
}
