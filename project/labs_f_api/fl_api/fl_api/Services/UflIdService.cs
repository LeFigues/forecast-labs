using fl_api.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace fl_api.Services;

public class UflIdService : IUflIdService
{
    private readonly HttpClient _httpClient;

    public UflIdService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.students.underflabs.com/");
    }

    public async Task<JsonArray> GetPeopleAsync()
    {
        var response = await _httpClient.GetAsync("api/sim");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to fetch people from ufl_id: {content}");

        var people = JsonNode.Parse(content)?["data"]?.AsArray();
        return people ?? new JsonArray();
    }
}
