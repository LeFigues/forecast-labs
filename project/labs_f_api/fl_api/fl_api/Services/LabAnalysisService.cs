using fl_api.DTOs;
using fl_api.Interfaces;

namespace fl_api.Services
{
    public class LabAnalysisService : ILabAnalysisService
    {
        private readonly IGptService _gptService;
        private readonly ILabAnalysisRepository _repository;

        public LabAnalysisService(IGptService gptService, ILabAnalysisRepository repository)
        {
            _gptService = gptService;
            _repository = repository;
        }

        public async Task<LabAnalysisDto> AnalyzeAndSaveAsync(IFormFile file)
        {
            var result = await _gptService.ExtractLabDataFromPdfAsync(file);
            await _repository.SaveAsync(result);
            return result;
        }
    }
}
