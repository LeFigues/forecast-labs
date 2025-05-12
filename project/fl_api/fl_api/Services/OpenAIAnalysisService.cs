using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using fl_api.Interfaces;
using fl_api.Models;
using fl_api.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using fl_api.Dtos;

namespace fl_api.Services
{
    public class OpenAIAnalysisService : IOpenAIAnalysisService
    {
        private readonly IPdfExtractionService _extractor;
        private readonly IOpenAIClient _client;
        private readonly IPromptService _prompts;
        private readonly IMongoDbService _mongo;
        private readonly OpenAISettings _settings;

        public OpenAIAnalysisService(
            IPdfExtractionService extractor,
            IOpenAIClient client,
            IPromptService prompts,
            IMongoDbService mongo,
            IOptions<OpenAISettings> opts)
        {
            _extractor = extractor;
            _client = client;
            _prompts = prompts;
            _mongo = mongo;
            _settings = opts.Value;
        }

        public async Task<JsonDocument> AnalyzeAsync(Guid id, string? model = null)
        {
            // 1) Extraer raw JSON
            var rawDoc = await _extractor.ExtractJsonAsync(id);
            var rawJson = rawDoc.RootElement.GetRawText();

            // 2) Corrección
            var corrPrompt = _prompts.GetPrompt("correction");
            var corrReq = new ChatCompletionRequest
            {
                Model = _settings.AssistantId,
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "system", Content = corrPrompt },
                    new ChatMessage { Role = "user",   Content = rawJson   }
                }
            };
            var corrRes = await _client.CreateChatCompletionAsync(corrReq);
            var corrected = corrRes.Choices[0].Message.Content.Trim();

            // 3) Estructura final
            var structPrompt = _prompts.GetPrompt("structuring");
            var chosenModel = string.IsNullOrWhiteSpace(model)
                                ? _settings.AssistantId
                                : model;
            var structReq = new ChatCompletionRequest
            {
                Model = chosenModel,
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "system", Content = structPrompt },
                    new ChatMessage { Role = "user",   Content = corrected    }
                }
            };
            var structRes = await _client.CreateChatCompletionAsync(structReq);
            var finalJson = structRes.Choices[0].Message.Content.Trim();

            // 4) Persistir solo finalJson + metadata
            var root = JsonDocument.Parse(finalJson).RootElement;
            var record = new PythonAnalysisRecord
            {
                DocumentId = id,
                FileName = root.GetProperty("file").GetString()!,
                Subject = root.GetProperty("subject").GetString()!,
                Code = root.GetProperty("code").GetString()!,
                Version = root.GetProperty("version").GetString()!,
                ValidFrom = DateTime.Parse(root.GetProperty("valid_from").GetString()!),
                PracticeNumber = root.GetProperty("practice_number").GetInt32(),
                Title = root.GetProperty("title").GetString()!,
                Groups = root.GetProperty("groups").GetInt32(),
                AnalysisResult = BsonDocument.Parse(finalJson),
                ModelUsed = chosenModel,
                AnalyzedAt = DateTime.UtcNow,
                SatisfactionPercentage = 0.0
            };
            var col = _mongo.GetCollection<PythonAnalysisRecord>("openai");
            await col.InsertOneAsync(record);

            // 5) Devolver como JsonDocument
            return JsonDocument.Parse(finalJson);
        }
    }
}
