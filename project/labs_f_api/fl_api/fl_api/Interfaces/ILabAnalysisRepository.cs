using fl_api.DTOs;

namespace fl_api.Interfaces
{
    public interface ILabAnalysisRepository
    {
        Task SaveAsync(LabAnalysisDto lab);
    }
}
