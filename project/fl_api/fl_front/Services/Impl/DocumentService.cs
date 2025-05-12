using fl_front.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _http;

        public DocumentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UploadResponse?> UploadPdfAsync(Stream fileStream, string fileName, int careerId, int subjectId)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            content.Add(fileContent, "file", fileName);
            content.Add(new StringContent(careerId.ToString()), "careerId");
            content.Add(new StringContent(subjectId.ToString()), "subjectId");

            var response = await _http.PostAsync("api/documents/send-pdf", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<UploadResponse>();
        }

        public async Task<AnalysisResponse?> AnalyzeWithOpenAIAsync(string documentId, string model = "gpt-4")
        {
            try
            {
                var url = $"api/analyze/openai/{documentId}?model={model}";
                var response = await _http.GetFromJsonAsync<AnalysisResponse>(url);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al analizar con OpenAI: {ex.Message}");
                return null;
            }
        }

        public async Task ClassifyDocumentAsync(string documentId, string subjectName)
        {
            var payload = new
            {
                documentId,
                subjectName
            };

            var response = await _http.PostAsJsonAsync("api/classification", payload);
            response.EnsureSuccessStatusCode(); // opcional: lanza excepción si falla
        }

    }
}
