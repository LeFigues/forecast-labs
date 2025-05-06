using fl_api.DTOs;
using fl_api.Interfaces;
using MongoDB.Driver;

namespace fl_api.Repositories
{
    public class LabForecastRepository : ILabForecastRepository
    {
        private readonly IMongoCollection<LabAnalysisResult> _collection;

        public LabForecastRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = database.GetCollection<LabAnalysisResult>("LabResults");
        }

        public async Task SaveAsync(LabAnalysisResult result)
        {
            await _collection.InsertOneAsync(result);
        }
    }
}
