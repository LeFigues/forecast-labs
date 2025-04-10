using fl_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("supplies")]
        public ActionResult<List<SupplyReport>> GetSimulatedSupplyReport()
        {
            var report = new List<SupplyReport>
            {
                new SupplyReport
                {
                    Name = "Cable de consola",
                    QuantityUsed = 20,
                    QuantityDamaged = 3,
                    QuantityConsumed = 17,
                    QuantityRequired = 25
                },
                new SupplyReport
                {
                    Name = "Cable Ethernet",
                    QuantityUsed = 30,
                    QuantityDamaged = 2,
                    QuantityConsumed = 28,
                    QuantityRequired = 35
                },
                new SupplyReport
                {
                    Name = "Router Cisco 4221",
                    QuantityUsed = 10,
                    QuantityDamaged = 1,
                    QuantityConsumed = 9,
                    QuantityRequired = 12
                },
                new SupplyReport
                {
                    Name = "Switch Cisco 2960",
                    QuantityUsed = 10,
                    QuantityDamaged = 0,
                    QuantityConsumed = 10,
                    QuantityRequired = 10
                },
                new SupplyReport
                {
                    Name = "PC con emulador de terminal (Tera Term)",
                    QuantityUsed = 20,
                    QuantityDamaged = 0,
                    QuantityConsumed = 20,
                    QuantityRequired = 20
                }
            };

            return Ok(report);
        }
    }
}
