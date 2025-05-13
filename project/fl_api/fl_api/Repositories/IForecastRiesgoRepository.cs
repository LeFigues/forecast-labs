using fl_api.Models;

namespace fl_api.Repositories
{
    public interface IForecastRiesgoRepository
    {
        Task SaveManyAsync(List<ForecastRiesgoRecord> records);
        Task<List<ForecastRiesgoRecord>> GetAllAsync();
    }
}
