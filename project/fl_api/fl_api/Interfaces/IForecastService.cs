using fl_api.Dtos;
using fl_api.Dtos.Forecast;

namespace fl_api.Interfaces
{
    public interface IForecastService
    {
        /// <param name="history">Serie plana de DailyDemandDto</param>
        /// <param name="horizon">"monthly" o "semestral"</param>
        Task<List<ForecastPointDto>> ForecastAsync(
            IEnumerable<DailyDemandDto> history,
            string horizon);

        Task<List<ForecastInsumoDto>> ForecastInsumosPorPracticaAsync();
        Task<List<ForecastHistoricoDto>> ForecastInsumosHistoricoAsync();
        Task<List<ForecastPracticaDto>> ForecastPracticasUsoAsync();
        Task<List<ForecastRiesgoDto>> ForecastInsumosEnRiesgoAsync();





    }
}
