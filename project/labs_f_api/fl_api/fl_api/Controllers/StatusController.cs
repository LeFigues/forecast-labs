using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystemStatus()
        {
            var mongoOk = await _statusService.IsMongoConnectedAsync();
            var gptOk = await _statusService.IsGptApiAvailableAsync();

            var result = new
            {
                MongoDB = mongoOk ? "Connected" : "❌ Connection failed",
                OpenAI = gptOk ? "Connected" : "❌ Connection failed",
                Timestamp = DateTime.UtcNow
            };

            return Ok(result);
        }
    }
}
