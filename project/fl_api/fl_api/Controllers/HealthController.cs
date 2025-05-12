using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Net;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IMongoDbService _mongoService;
        private readonly IOpenAIClient _openAiClient;
        private readonly IStudentsApiClient _studentsClient;
        private readonly ILabsApiClient _labsClient;
        private readonly PythonConfigRoutes _pythonRoutes;

        public HealthController(
            IMongoDbService mongoService,
            IOpenAIClient openAiClient,
            IStudentsApiClient studentsClient,
            ILabsApiClient labsClient,
            IOptions<PythonConfigRoutes> pythonRoutes
        )
        {
            _mongoService = mongoService;
            _openAiClient = openAiClient;
            _studentsClient = studentsClient;
            _labsClient = labsClient;
            _pythonRoutes = pythonRoutes.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = new Dictionary<string, object>();

            // 1) MongoDB health
            var mongoStatus = "Healthy";
            string mongoDetail = "OK";
            try
            {
                var db = _mongoService.GetCollection<BsonDocument>("HealthCheck").Database;
                var cmd = new BsonDocument("ping", 1);
                await db.RunCommandAsync<BsonDocument>(cmd);
            }
            catch (Exception ex)
            {
                mongoStatus = "Unhealthy";
                mongoDetail = ex.Message;
            }
            results["MongoDb"] = new { Status = mongoStatus, Detail = mongoDetail };

            // 2) OpenAI health
            var openAiStatus = "Healthy";
            string openAiDetail = "OK";
            try
            {
                var req = new ChatCompletionRequest
                {
                    Messages = new List<ChatMessage>
                    {
                        new ChatMessage { Role = "system", Content = "Health check" }
                    }
                };
                await _openAiClient.CreateChatCompletionAsync(req);
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // OK: credentials valid but no content (treated as healthy ping)
            }
            catch (Exception ex)
            {
                openAiStatus = "Unhealthy";
                openAiDetail = ex.Message;
            }
            results["OpenAI"] = new { Status = openAiStatus, Detail = openAiDetail };

            // 3) Students API health (usando /api/health)
            var studentsStatus = "Healthy";
            string studentsDetail = "OK";
            try
            {
                var resp = await _studentsClient.PingAsync();
                resp.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException httpEx)
            {
                studentsStatus = "Unhealthy";
                studentsDetail = $"HTTP {(int)httpEx.StatusCode}: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                studentsStatus = "Unhealthy";
                studentsDetail = ex.Message;
            }
            results["StudentsApi"] = new { Status = studentsStatus, Detail = studentsDetail };

            // 4) Labs API health
            var labsStatus = "Healthy";
            string labsDetail = "OK";
            try
            {
                await _labsClient.GetLabAsync("nonexistent-id");
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // OK
            }
            catch (Exception ex)
            {
                labsStatus = "Unhealthy";
                labsDetail = ex.Message;
            }
            results["LabsApi"] = new { Status = labsStatus, Detail = labsDetail };

            // 5) Python file access
            var pyPaths = new Dictionary<string, bool>();
            string basePath = _pythonRoutes.BasePath;
            pyPaths["InputFolderExists"] = System.IO.Directory.Exists(System.IO.Path.Combine(basePath, _pythonRoutes.InputFolder));
            pyPaths["OutputFolderExists"] = System.IO.Directory.Exists(System.IO.Path.Combine(basePath, _pythonRoutes.OutputFolder));
            pyPaths["PermanentFolderExists"] = System.IO.Directory.Exists(System.IO.Path.Combine(basePath, _pythonRoutes.PermanentFolder));
            pyPaths["ScriptExtractJsonExists"] = System.IO.File.Exists(System.IO.Path.Combine(basePath, _pythonRoutes.ScriptExtractJson));
            pyPaths["PythonExeExists"] = System.IO.File.Exists(System.IO.Path.Combine(basePath, _pythonRoutes.PythonExe));
            results["PythonFiles"] = pyPaths;

            // Overall status
            bool allHealthy = true;
            foreach (var kv in results)
            {
                if (kv.Value is IDictionary<string, bool> dict)
                {
                    if (dict.Values.Any(v => !v))
                    {
                        allHealthy = false;
                        break;
                    }
                }
                else
                {
                    var statusProp = kv.Value.GetType().GetProperty("Status");
                    var status = statusProp?.GetValue(kv.Value)?.ToString();
                    if (status != "Healthy")
                    {
                        allHealthy = false;
                        break;
                    }
                }
            }

            return Ok(new { OverallStatus = allHealthy ? "Healthy" : "Unhealthy", Checks = results });
        }
    }
}
