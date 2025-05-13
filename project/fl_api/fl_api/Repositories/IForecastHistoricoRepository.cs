using fl_api.Models;

namespace fl_api.Repositories
{
    public interface IForecastHistoricoRepository
    {
        Task SaveManyAsync(List<ForecastHistoricoRecord> records);
        Task<List<ForecastHistoricoRecord>> GetAllAsync();
    }
}
