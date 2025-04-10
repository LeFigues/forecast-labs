using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly ILabAnalysisService _labAnalysisService;

    public PdfController(ILabAnalysisService labAnalysisService)
    {
        _labAnalysisService = labAnalysisService;
    }

    /// <summary>
    /// Analyzes a laboratory PDF using OpenAI Assistant and stores the result in MongoDB.
    /// </summary>
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

    /// <summary>
    /// Simple ping to confirm controller is working.
    /// </summary>
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("PdfController is alive ✅");
}
