using fl_api.DTOs;
using fl_api.Interfaces;
using fl_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IGptService _gptService;
        private readonly ILabAnalysisService _labAnalysisService;
        public PdfController(IGptService gptService, ILabAnalysisService labAnalysisService)
        {
            _gptService = gptService;
            _labAnalysisService = labAnalysisService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<LabAnalysisDto>> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid PDF file.");

            try
            {
                var analysis = await _gptService.ExtractLabDataFromPdfAsync(file);
                return Ok(analysis);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to process file: {ex.Message}");
            }
        }
        [HttpPost("analyze-and-save")]
        public async Task<IActionResult> AnalyzeAndSave(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid PDF file.");

            try
            {
                var result = await _labAnalysisService.AnalyzeAndSaveAsync(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to analyze and save lab data: {ex.Message}");
            }
        }
    }
}
