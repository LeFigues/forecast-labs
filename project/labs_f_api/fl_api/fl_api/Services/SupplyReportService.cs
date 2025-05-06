using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Driver;

namespace fl_api.Services
{
    public class SupplyReportService : ISupplyReportService
    {
        private readonly IMongoCollection<SupplyReport> _collection;

        public SupplyReportService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = database.GetCollection<SupplyReport>("SupplyReports");
        }

        public async Task<List<SupplyReport>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<SupplyReport?> GetByIdAsync(string id) =>
            await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(SupplyReport report) =>
            await _collection.InsertOneAsync(report);

        public async Task UpdateAsync(string id, SupplyReport report) =>
            await _collection.ReplaceOneAsync(r => r.Id == id, report);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(r => r.Id == id);
    }

}
