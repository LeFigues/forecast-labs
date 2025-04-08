using fl_api.DTOs;

namespace fl_api.Interfaces
{
    public interface ILabAnalysisService
    {
        Task<LabAnalysisDto> AnalyzeAndSaveAsync(IFormFile file);
    }
}
