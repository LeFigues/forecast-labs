using fl_api.Configurations;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Services
{
    public class StatusService : IStatusService
    {
        private readonly MongoDbSettings _mongoSettings;
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _openAiSettings;

        public StatusService(
            IOptions<MongoDbSettings> mongoOptions,
            IOptions<OpenAISettings> openAiOptions,
            HttpClient httpClient)
        {
            _mongoSettings = mongoOptions.Value;
            _openAiSettings = openAiOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<bool> IsMongoConnectedAsync()
        {
            try
            {
                var client = new MongoClient(_mongoSettings.ConnectionString);
                var database = client.GetDatabase(_mongoSettings.DatabaseName);
                var command = new BsonDocument("ping", 1);
                await database.RunCommandAsync<BsonDocument>(command);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsGptApiAvailableAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _openAiSettings.BaseUrl + "/models");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _openAiSettings.ApiKey);
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
