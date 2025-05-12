using fl_front.Models;

namespace fl_front.Services
{
    public interface IDemandReportService
    {
        Task<DemandSummary?> GetSummaryAsync(DateTime from, DateTime to);
        Task<List<DemandDetail>> GetDetailAsync(DateTime from, DateTime to);
        Task<DemandSimulation?> GetSimulationAsync(DateTime from, DateTime to);
        Task<List<DemandHistoryItem>> GetHistoryAsync(DateTime from, DateTime to);
    }
}
