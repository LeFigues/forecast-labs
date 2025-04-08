using fl_api.Configurations;
using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace fl_api.Services
{
    public class GptService : IGptService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _settings;

        public GptService(HttpClient httpClient, IOptions<OpenAISettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<LabAnalysisDto> ExtractLabDataFromPdfAsync(IFormFile pdfFile)
        {
            // Upload PDF to OpenAI
            using var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(pdfFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            content.Add(streamContent, "file", pdfFile.FileName);
            content.Add(new StringContent("assistants"), "purpose");

            var uploadResponse = await _httpClient.PostAsync("files", content);

            var uploadJson = await uploadResponse.Content.ReadAsStringAsync();

            if (!uploadResponse.IsSuccessStatusCode)
                throw new Exception($"Failed to upload PDF to OpenAI: {uploadJson}");

            var fileId = JsonDocument.Parse(uploadJson)
                            .RootElement.GetProperty("id").GetString();

            // Prompt
            var prompt = """
        Analyze the attached PDF, which contains a laboratory guide. Return ONLY a JSON using this structure:

        {
          "laboratory": "LAB NAME",
          "title": "PRACTICE TITLE",
          "groups": N,
          "materials": {
            "equipment": [
              {
                "quantity_per_group": N,
                "unit": "Unit",
                "description": "Equipment description"
              }
            ],
            "supplies": [
              {
                "quantity_per_group": N,
                "unit": "Unit",
                "description": "Supply description"
              }
            ]
          }
        }

        Do not include any explanations or text, just valid JSON.
        """;

            // GPT call
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are an expert in academic laboratory guides." },
                new { role = "user", content = prompt }
            },
                file_ids = new[] { fileId },
                temperature = 0.2
            };

            var requestJson = JsonSerializer.Serialize(request);
            var requestContent = new StringContent(requestJson);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("chat/completions", requestContent);

            var resultJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to get response from OpenAI: {resultJson}");

            var json = JsonDocument.Parse(resultJson);
            var message = json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            // Deserialize the JSON
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<LabAnalysisDto>(message!, options)
                         ?? throw new Exception("Failed to deserialize GPT response.");

            return result;
        }
    }
}
