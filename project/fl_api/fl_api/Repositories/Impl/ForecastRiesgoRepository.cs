using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Driver;

namespace fl_api.Repositories.Impl
{
    public class ForecastRiesgoRepository : IForecastRiesgoRepository
    {
        private readonly IMongoCollection<ForecastRiesgoRecord> _collection;

        public ForecastRiesgoRepository(IMongoDbService mongo)
        {
            _collection = mongo.GetCollection<ForecastRiesgoRecord>("forecast_riesgo");
        }

        public async Task SaveManyAsync(List<ForecastRiesgoRecord> records)
        {
            if (records.Any())
                await _collection.InsertManyAsync(records);
        }

        public async Task<List<ForecastRiesgoRecord>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
