using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IForecastService
    {
        /// <param name="history">Serie plana de DailyDemandDto</param>
        /// <param name="horizon">"monthly" o "semestral"</param>
        Task<List<ForecastPointDto>> ForecastAsync(
            IEnumerable<DailyDemandDto> history,
            string horizon);
    }
}
