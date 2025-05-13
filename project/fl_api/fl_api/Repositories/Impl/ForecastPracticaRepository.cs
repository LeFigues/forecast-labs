using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Driver;

namespace fl_api.Repositories.Impl
{
    public class ForecastPracticaRepository : IForecastPracticaRepository
    {
        private readonly IMongoCollection<ForecastPracticaRecord> _collection;

        public ForecastPracticaRepository(IMongoDbService mongo)
        {
            _collection = mongo.GetCollection<ForecastPracticaRecord>("forecast_practicas");
        }

        public async Task SaveManyAsync(List<ForecastPracticaRecord> records)
        {
            if (records.Any())
                await _collection.InsertManyAsync(records);
        }

        public async Task<List<ForecastPracticaRecord>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
