using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly IPlanningRepository _repository;

        public PlanningController(IPlanningRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPlannings()
        {
            try
            {
                var plannings = await _repository.GetAllAsync();
                return Ok(plannings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching plannings: {ex.Message}");
            }
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetPlanningById(string id)
        {
            try
            {
                var planning = await _repository.GetByIdAsync(id);
                if (planning == null)
                    return NotFound(new { message = "Planning not found." });

                return Ok(planning);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching planning: {ex.Message}");
            }
        }


        [HttpPost("insert")]
        public async Task<IActionResult> CreatePlanning([FromBody] PlanningDto planning)
        {
            try
            {
                await _repository.CreateAsync(planning);
                return Ok(new { message = "Planning registered successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error insertando en MongoDB: " + ex.Message);
                return StatusCode(500, $"Error creating planning: {ex.Message}");
            }
        }

    }
}
