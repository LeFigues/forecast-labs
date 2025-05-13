using fl_front.Dtos;
using fl_front.Models;

namespace fl_front.Services
{
    public interface IForecastService
    {
        Task<List<ForecastResult>> GetForecastAsync(ForecastRequest request);
        Task<List<ForecastHistoricoDto>> GetForecastHistoricoAsync();
        Task<List<ForecastPracticaDto>> GetForecastPracticasUsoAsync();
        Task<List<ForecastRiesgoDto>> GetForecastRiesgoAsync();
    }
}
