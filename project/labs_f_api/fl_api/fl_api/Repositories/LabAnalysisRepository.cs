using fl_api.Configurations;
using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace fl_api.Repositories
{
    public class LabAnalysisRepository : ILabAnalysisRepository
    {
        private readonly IMongoCollection<LabAnalysisDto> _collection;

        public LabAnalysisRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<LabAnalysisDto>("lab_analyses");
        }

        public async Task SaveAsync(LabAnalysisDto lab)
        {
            await _collection.InsertOneAsync(lab);
        }
    }
}
