using fl_api.Models;

namespace fl_api.Repositories
{
    public interface IForecastPracticaRepository
    {
        Task SaveManyAsync(List<ForecastPracticaRecord> records);
        Task<List<ForecastPracticaRecord>> GetAllAsync();
    }
}
