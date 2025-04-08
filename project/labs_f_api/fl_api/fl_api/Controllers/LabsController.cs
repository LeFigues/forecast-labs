using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabsController : ControllerBase
    {
        private readonly ILabAnalysisRepository _repo;

        public LabsController(ILabAnalysisRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("LabsController is working ✅");
        }
        [HttpPost("save-simulated")]
        public async Task<IActionResult> SaveSimulatedLab()
        {
            try
            {
                var simulated = new LabAnalysisDto
                {
                    Laboratory = "REDES Y COMUNICACIÓN DE DATOS II - PRÁCTICA 1",
                    Title = "Configuración básica de un switch y configuración básica de un router",
                    Groups = 10,
                    Materials = new MaterialsDto
                    {
                        Equipment = new List<MaterialItemDto>
                {
                    new() { QuantityPerGroup = 1, Unit = "Pza", Description = "Router Cisco 4221 con imagen universal Cisco IOS XE versión 16.9.4 o comparable" },
                    new() { QuantityPerGroup = 1, Unit = "Pza", Description = "Switch Cisco 2960 con Cisco IOS versión 15.0(2), imagen lanbasek9 o comparable" },
                    new() { QuantityPerGroup = 2, Unit = "Pza", Description = "PC con Windows y programa de emulación de terminal (como Tera Term)" }
                },
                        Supplies = new List<MaterialItemDto>
                {
                    new() { QuantityPerGroup = 2, Unit = "Pza", Description = "Cable de consola" },
                    new() { QuantityPerGroup = 3, Unit = "Pza", Description = "Cable Ethernet" }
                }
                    }
                };

                await _repo.SaveAsync(simulated);

                return Ok(new { message = "Simulated lab analysis saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
            
        }
    }
}
