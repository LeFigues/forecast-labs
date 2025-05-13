using fl_api.Dtos;
using fl_api.Dtos.Forecast;
using fl_api.Interfaces;
using fl_api.Models;
using fl_api.Repositories;
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

        [HttpGet("insumos-por-practica")]
        public async Task<ActionResult<List<ForecastInsumoDto>>> GetForecast()
        {
            var data = await _forecastSvc.ForecastInsumosPorPracticaAsync();
            return Ok(data);
        }
        [HttpGet("insumos-historico")]
        public async Task<ActionResult<List<ForecastHistoricoDto>>> GetForecastHistorico()
        {
            var data = await _forecastSvc.ForecastInsumosHistoricoAsync();
            return Ok(data);
        }
        [HttpGet("practicas-uso")]
        public async Task<ActionResult<List<ForecastPracticaDto>>> GetForecastPracticasUso()
        {
            var data = await _forecastSvc.ForecastPracticasUsoAsync();
            return Ok(data);
        }
        [HttpGet("insumos-riesgo")]
        public async Task<ActionResult<List<ForecastRiesgoDto>>> GetForecastRiesgo()
        {
            var data = await _forecastSvc.ForecastInsumosEnRiesgoAsync();
            return Ok(data);
        }
        [HttpGet("insumos-historico/historico")]
        public async Task<ActionResult<List<ForecastHistoricoRecord>>> GetHistoricoInsumosMongo(
    [FromServices] IForecastHistoricoRepository repo)
        {
            var data = await repo.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("practicas-uso/historico")]
        public async Task<ActionResult<List<ForecastPracticaRecord>>> GetHistoricoPracticasMongo(
            [FromServices] IForecastPracticaRepository repo)
        {
            var data = await repo.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("insumos-riesgo/ai")]
        public async Task<ActionResult<string>> GetRiesgoConIA(
    [FromBody] ForecastAiRequest request,
    [FromServices] IOpenAIClient ai,
    [FromServices] IPromptService prompts)
        {
            var resumen = string.Join("\n", request.Datos.Select(d =>
                $"- {d.InsumoNombre}: stock {d.StockActual}, uso promedio {d.UsoMensualPromedio}, meses restantes {d.MesesSobrantes} ({d.Riesgo})"));

            var promptText = prompts.GetPrompt("prediction");
            var input = $"{promptText}\n\n{resumen}";

            var response = await ai.CreateChatCompletionAsync(new ChatCompletionRequest
            {
                Messages = new List<ChatMessage>
        {
            new ChatMessage { Role = "system", Content = "Eres un analista de logística..." },
            new ChatMessage { Role = "user", Content = input }

        }
            });

            return Ok(response.GetText());
        }

    }
}
