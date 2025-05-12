using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace fl_api.Services
{
    public class OpenAIClient : IOpenAIClient
    {
        private readonly HttpClient _http;
        private readonly OpenAISettings _settings;

        public OpenAIClient(HttpClient http, IOptions<OpenAISettings> opts)
        {
            _settings = opts.Value;
            _http = http;
            // BaseAddress ya quedó configurada en Program.cs
        }

        // /Services/OpenAIClient.cs
        public async Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request)
        {
            // Sólo asignamos el default si NO se pasó explicitamente un model
            request.Model = string.IsNullOrWhiteSpace(request.Model)
                ? _settings.AssistantId
                : request.Model;

            var response = await _http.PostAsJsonAsync("/v1/chat/completions", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            if (result == null) throw new Exception("OpenAI devolvió un cuerpo vacío.");
            return result;
        }

    }
}
