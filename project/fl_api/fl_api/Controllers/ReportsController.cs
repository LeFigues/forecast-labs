using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IDemandReportService _reportService;
        private readonly IPurchaseSimulationService _simService;
        public ReportsController(IDemandReportService reportService, IPurchaseSimulationService simService)
        {
            _reportService = reportService;
            _simService = simService;
        }

        /// <summary>
        /// GET /api/reports/demand?from=2025-05-11&to=2025-05-11
        /// </summary>
        [HttpGet("demand-detail")]
        public async Task<ActionResult<List<DemandReportDetailDto>>> GetDemandDetail(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to)
                return BadRequest(new { message = "'from' debe ser anterior o igual a 'to'" });

            var report = await _reportService.GetDemandReportDetailAsync(from, to);
            return Ok(report);
        }

        [HttpGet("demand-summary")]
        public async Task<ActionResult<DemandSummaryDto>> GetDemandSummary(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to) return BadRequest(new { message = "'from' debe ser <= 'to'" });
            var summary = await _reportService.GetDemandSummaryAsync(from, to);
            return Ok(summary);
        }

        [HttpGet("demand-simulation")]
        public async Task<ActionResult<PurchaseSimulationDto>> GetSimulation(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to)
                return BadRequest(new { message = "'from' debe ser <= 'to'" });

            var sim = await _simService.SimulateAsync(from, to);
            return Ok(sim);
        }

        [HttpGet("demand-history")]
        public async Task<IActionResult> GetDemandHistory(
    [FromQuery] DateTime from,
    [FromQuery] DateTime to)
        {
            if (from > to)
                return BadRequest(new { message = "'from' debe ser menor o igual que 'to'" });

            var history = await _reportService.GetDemandHistoryAsync(from, to);
            return Ok(history);
        }

    }
}
