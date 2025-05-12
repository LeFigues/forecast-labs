using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IDemandReportService
    {
        /// <summary>
        /// Agrega demanda de insumos entre fechas (inclusive), agrupado por carrera y materia.
        /// </summary>
        Task<List<DemandReportDetailDto>> GetDemandReportDetailAsync(DateTime from, DateTime to);
        Task<DemandSummaryDto> GetDemandSummaryAsync(DateTime from, DateTime to);

        Task<List<DailyDemandDto>> GetDemandHistoryAsync(DateTime from, DateTime to);

    }
}
