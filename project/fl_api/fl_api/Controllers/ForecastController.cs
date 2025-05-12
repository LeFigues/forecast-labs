using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly IDemandReportService _reportSvc;
        private readonly IForecastService _forecastSvc;

        public ForecastController(
            IDemandReportService reportSvc,
            IForecastService forecastSvc)
        {
            _reportSvc = reportSvc;
            _forecastSvc = forecastSvc;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ForecastRequestDto dto)
        {
            // 1) Obtén la historia agregada por periodos
            var history = await _reportSvc.GetDemandHistoryAsync(dto.From, dto.To);
            if (!history.Any())
                return Ok(Array.Empty<ForecastPointDto>());

            // 2) Lanza el forecast
            var forecast = await _forecastSvc.ForecastAsync(history, dto.Horizon);

            return Ok(forecast);
        }
    }
}
