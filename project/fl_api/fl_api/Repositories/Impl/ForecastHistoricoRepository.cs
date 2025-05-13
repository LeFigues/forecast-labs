using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Driver;

namespace fl_api.Repositories.Impl
{
    public class ForecastHistoricoRepository : IForecastHistoricoRepository
    {
        private readonly IMongoCollection<ForecastHistoricoRecord> _collection;

        public ForecastHistoricoRepository(IMongoDbService mongo)
        {
            _collection = mongo.GetCollection<ForecastHistoricoRecord>("forecast_historico");
        }

        public async Task SaveManyAsync(List<ForecastHistoricoRecord> records)
        {
            if (records.Any())
                await _collection.InsertManyAsync(records);
        }

        public async Task<List<ForecastHistoricoRecord>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
