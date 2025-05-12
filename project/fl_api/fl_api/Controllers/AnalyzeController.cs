using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using fl_api.Interfaces;
using fl_api.Models;
using fl_api.Dtos;
using fl_api.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyzeController : ControllerBase
    {
        private readonly IPythonAnalyzerService _pythonAnalyzer;
        private readonly IPdfExtractionService _pdfExtractor;
        private readonly IOpenAIClient _openAiClient;
        private readonly IPromptService _promptService;
        private readonly IMongoDbService _mongo;
        private readonly OpenAISettings _openAiSettings;

        public AnalyzeController(
            IPythonAnalyzerService pythonAnalyzer,
            IPdfExtractionService pdfExtractor,
            IOpenAIClient openAiClient,
            IPromptService promptService,
            IMongoDbService mongo,
            IOptions<OpenAISettings> openAiOptions)
        {
            _pythonAnalyzer = pythonAnalyzer;
            _pdfExtractor = pdfExtractor;
            _openAiClient = openAiClient;
            _promptService = promptService;
            _mongo = mongo;
            _openAiSettings = openAiOptions.Value;
        }

        // Helper para leer int aunque venga como string
        private static int ReadInt(JsonElement el) => el.ValueKind switch
        {
            JsonValueKind.Number => el.GetInt32(),
            JsonValueKind.String => int.Parse(el.GetString()!),
            _ => throw new InvalidOperationException($"Expected Number or String, got {el.ValueKind}")
        };

        /// <summary>
        /// 1) Python-only: invoca tu PyAnalyze.py y devuelve el JSON crudo.
        /// </summary>
        [HttpGet("python/{id:guid}")]
        public async Task<IActionResult> AnalyzeWithPython(Guid id)
        {
            try
            {
                var docJson = await _pythonAnalyzer.AnalyzeWithPythonAsync(id);
                return Content(docJson.RootElement.GetRawText(), "application/json");
            }
            catch (FileNotFoundException fnf) { return NotFound(new { message = fnf.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        /// <summary>
        /// 2) Python+OpenAI: toma el JSON crudo de Python, lo corrige y formatea con OpenAI, lo guarda y lo devuelve.
        /// </summary>
        [HttpGet("python-openai/{id:guid}")]
        public async Task<IActionResult> AnalyzeWithPythonAndOpenAI(Guid id, [FromQuery] string? model = null)
        {
            // 1) JSON crudo desde PyAnalyze.py
            JsonDocument pyDoc;
            try { pyDoc = await _pythonAnalyzer.AnalyzeWithPythonAsync(id); }
            catch (FileNotFoundException fnf) { return NotFound(new { message = fnf.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }

            var rawJson = pyDoc.RootElement.GetRawText();

            // 2) Corrección
            var corrPrompt = _promptService.GetPrompt("correction");
            var corrRes = await _openAiClient.CreateChatCompletionAsync(new ChatCompletionRequest
            {
                Model = _openAiSettings.AssistantId,
                Messages = new List<ChatMessage> {
                    new ChatMessage{ Role="system", Content=corrPrompt },
                    new ChatMessage{ Role="user",   Content=rawJson   }
                }
            });
            var correctedJson = corrRes.Choices[0].Message.Content.Trim();

            // 3) Estructuración
            var structPrompt = _promptService.GetPrompt("structuring");
            var chosenModel = string.IsNullOrWhiteSpace(model)
                                 ? _openAiSettings.AssistantId
                                 : model;
            var structRes = await _openAiClient.CreateChatCompletionAsync(new ChatCompletionRequest
            {
                Model = chosenModel,
                Messages = new List<ChatMessage> {
                    new ChatMessage{ Role="system", Content=structPrompt },
                    new ChatMessage{ Role="user",   Content=correctedJson }
                }
            });
            var finalJson = structRes.Choices[0].Message.Content.Trim();

            // 4) Parsear, persistir y devolver
            return await PersistAndReturnAsync(finalJson, id, chosenModel);
        }

        /// <summary>
        /// 3) OpenAI-only: extrae todo el PDF a JSON, lo formatea con OpenAI, lo guarda y lo devuelve.
        /// </summary>
        [HttpGet("openai/{id:guid}")]
        public async Task<IActionResult> AnalyzeOpenAIOnly(Guid id, [FromQuery] string? model = null)
        {
            // 1) JSON crudo desde extractor general
            JsonDocument rawDoc;
            try { rawDoc = await _pdfExtractor.ExtractJsonAsync(id); }
            catch (FileNotFoundException fnf) { return NotFound(new { message = fnf.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }

            var rawJson = rawDoc.RootElement.GetRawText();

            // 2) Corrección
            var corrPrompt = _promptService.GetPrompt("correction");
            var corrRes = await _openAiClient.CreateChatCompletionAsync(new ChatCompletionRequest
            {
                Model = _openAiSettings.AssistantId,
                Messages = new List<ChatMessage> {
                    new ChatMessage{ Role="system", Content=corrPrompt },
                    new ChatMessage{ Role="user",   Content=rawJson   }
                }
            });
            var correctedJson = corrRes.Choices[0].Message.Content.Trim();

            // 3) Estructuración
            var structPrompt = _promptService.GetPrompt("structuring");
            var chosenModel = string.IsNullOrWhiteSpace(model)
                                 ? _openAiSettings.AssistantId
                                 : model;
            var structRes = await _openAiClient.CreateChatCompletionAsync(new ChatCompletionRequest
            {
                Model = chosenModel,
                Messages = new List<ChatMessage> {
                    new ChatMessage{ Role="system", Content=structPrompt },
                    new ChatMessage{ Role="user",   Content=correctedJson }
                }
            });
            var finalJson = structRes.Choices[0].Message.Content.Trim();

            // 4) Parsear, persistir y devolver
            return await PersistAndReturnAsync(finalJson, id, chosenModel);
        }

        /// <summary>
        /// Helper para parsear el JSON final, persistir en collection "openai" y devolver el DTO.
        /// </summary>
        private async Task<IActionResult> PersistAndReturnAsync(string finalJson, Guid documentId, string modelUsed)
        {
            var root = JsonDocument.Parse(finalJson).RootElement;
            var analysisEl = root.GetProperty("analysisResult");

            // Extraemos valid_from de forma tolerante
            DateTime validFrom;
            var validFromProp = root.GetProperty("valid_from").GetString();
            if (string.IsNullOrWhiteSpace(validFromProp) ||
                !DateTime.TryParse(validFromProp, out validFrom))
            {
                // Valor por defecto si viene vacío o mal formateado
                validFrom = DateTime.MinValue;
            }

            var record = new PythonAnalysisRecord
            {
                DocumentId = documentId,
                FileName = root.GetProperty("file").GetString()!,
                Subject = root.GetProperty("subject").GetString()!,
                Code = root.GetProperty("code").GetString()!,
                Version = root.GetProperty("version").GetString()!,
                ValidFrom = validFrom,
                PracticeNumber = ReadInt(root.GetProperty("practice_number")),
                Title = root.GetProperty("title").GetString()!,
                Groups = ReadInt(root.GetProperty("groups")),
                AnalysisResult = BsonDocument.Parse(analysisEl.GetRawText()),
                ModelUsed = modelUsed,
                AnalyzedAt = DateTime.UtcNow,
                SatisfactionPercentage = 0.0
            };

            await _mongo.GetCollection<PythonAnalysisRecord>("openai")
                        .InsertOneAsync(record);

            var dto = new PythonAnalysisResponseDto
            {
                Id = record.Id.ToString(),
                DocumentId = record.DocumentId,
                FileName = record.FileName,
                Subject = record.Subject,
                Code = record.Code,
                Version = record.Version,
                ValidFrom = record.ValidFrom,
                PracticeNumber = record.PracticeNumber,
                Title = record.Title,
                Groups = record.Groups,
                AnalysisResult = JsonDocument
                                           .Parse(record.AnalysisResult.ToJson())
                                           .RootElement,
                ModelUsed = record.ModelUsed,
                AnalyzedAt = record.AnalyzedAt,
                SatisfactionPercentage = record.SatisfactionPercentage
            };

            return Ok(dto);
        }


    }
}
