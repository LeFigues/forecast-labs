using fl_api.Configurations;
using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace fl_api.Services;

public class GptService : IGptService
{
    private readonly HttpClient _httpClient;
    private readonly OpenAISettings _settings;

    public GptService(HttpClient httpClient, IOptions<OpenAISettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;

        _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
    }

    public async Task<LabAnalysisDto> ExtractLabDataFromPdfAsync(IFormFile pdfFile)
    {
        // 1. Upload PDF
        using var content = new MultipartFormDataContent();
        var fileStream = new StreamContent(pdfFile.OpenReadStream());
        fileStream.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        content.Add(fileStream, "file", pdfFile.FileName);
        content.Add(new StringContent("assistants"), "purpose");

        var uploadRes = await _httpClient.PostAsync("files", content);
        var uploadJson = await uploadRes.Content.ReadAsStringAsync();

        if (!uploadRes.IsSuccessStatusCode)
            throw new Exception($"Upload failed: {uploadJson}");

        var fileId = JsonDocument.Parse(uploadJson)
            .RootElement.GetProperty("id").GetString();

        // 2. Create thread
        var threadRes = await _httpClient.PostAsync("threads", null);
        var threadJson = await threadRes.Content.ReadAsStringAsync();

        if (!threadRes.IsSuccessStatusCode)
            throw new Exception($"Thread creation failed: {threadJson}");

        var threadId = JsonDocument.Parse(threadJson)
            .RootElement.GetProperty("id").GetString();

        // 3. Send message (with file_attachments instead of file_ids)
        var prompt = """
        Analyze the attached laboratory guide (PDF) and return ONLY a valid JSON with this structure:

        {
          "laboratory": "...",
          "title": "...",
          "groups": N,
          "materials": {
            "equipment": [
              {
                "quantity_per_group": N,
                "unit": "...",
                "description": "..."
              }
            ],
            "supplies": [
              {
                "quantity_per_group": N,
                "unit": "...",
                "description": "..."
              }
            ]
          }
        }

        Do not include any explanations or comments. Only return the JSON.
        """;

        var msg = new
        {
            role = "user",
            content = prompt,
            attachments = new[]
            {
                new
                {
                    file_id = fileId,
                    tools = new[]
                    {
                        new { type = "file_search" }
                    }
                }
            }
        };

        var msgRequest = new HttpRequestMessage(HttpMethod.Post, $"threads/{threadId}/messages")
        {
            Content = new StringContent(JsonSerializer.Serialize(msg))
        };
        msgRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        msgRequest.Headers.Add("OpenAI-Beta", "assistants=v2");

        var msgRes = await _httpClient.SendAsync(msgRequest);
        var msgJson = await msgRes.Content.ReadAsStringAsync();

        if (!msgRes.IsSuccessStatusCode)
            throw new Exception($"Message error: {msgJson}");

        // 4. Create run
        var runBody = new
        {
            assistant_id = "asst_XrsDduEqtwJh7VioaGqLpVXw", // ← Reemplázalo con tu Assistant real
            instructions = "Analyze and extract structured data from the uploaded PDF."
        };

        var runContent = new StringContent(JsonSerializer.Serialize(runBody));
        runContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var runRes = await _httpClient.PostAsync($"threads/{threadId}/runs", runContent);
        var runJson = await runRes.Content.ReadAsStringAsync();

        if (!runRes.IsSuccessStatusCode)
            throw new Exception($"Run creation failed: {runJson}");

        var runId = JsonDocument.Parse(runJson)
            .RootElement.GetProperty("id").GetString();

        // 5. Wait until run is completed
        string status = "queued";
        while (status is "queued" or "in_progress")
        {
            await Task.Delay(1500);
            var check = await _httpClient.GetAsync($"threads/{threadId}/runs/{runId}");
            var checkJson = await check.Content.ReadAsStringAsync();
            status = JsonDocument.Parse(checkJson)
                .RootElement.GetProperty("status").GetString();
        }

        if (status != "completed")
            throw new Exception($"Run failed or was cancelled. Final status: {status}");

        // 6. Get last message
        var messagesRes = await _httpClient.GetAsync($"threads/{threadId}/messages");
        var messagesJson = await messagesRes.Content.ReadAsStringAsync();

        var root = JsonDocument.Parse(messagesJson).RootElement;

        var contentText = root
            .GetProperty("data")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetProperty("value")
            .GetString();

        if (string.IsNullOrWhiteSpace(contentText))
            throw new Exception("No content returned by GPT Assistant.");

        // 7. Deserialize to DTO
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var result = JsonSerializer.Deserialize<LabAnalysisDto>(contentText!, options)
                     ?? throw new Exception("Failed to deserialize GPT response.");

        return result;
    }
}
