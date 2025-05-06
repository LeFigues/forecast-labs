using fl_api.DTOs;

namespace fl_api.Interfaces
{
    public interface ILabForecastRepository
    {
        Task SaveAsync(LabAnalysisResult result);
    }
}
