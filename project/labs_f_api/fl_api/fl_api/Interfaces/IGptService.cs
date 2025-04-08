using fl_api.DTOs;
using Microsoft.AspNetCore.Http;
namespace fl_api.Interfaces
{
    public interface IGptService
    {
        Task<LabAnalysisDto> ExtractLabDataFromPdfAsync(IFormFile pdfFile);

    }
}
