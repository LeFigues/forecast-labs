using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// GET /api/health
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow.ToString("o")
            });
        }
    }
}
