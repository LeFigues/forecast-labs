using fl_api.Configurations;
using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace fl_api.Repositories
{
    public class PlanningRepository : IPlanningRepository
    {
        private readonly IMongoCollection<PlanningDto> _collection;

        public PlanningRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<PlanningDto>("planning");
        }

        public async Task CreateAsync(PlanningDto planning)
        {
            await _collection.InsertOneAsync(planning);
        }

        public async Task<List<PlanningDto>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
