using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UflPeopleController : ControllerBase
    {
        private readonly IUflIdService _uflService;

        public UflPeopleController(IUflIdService uflService)
        {
            _uflService = uflService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var people = await _uflService.GetPeopleAsync();
                return Ok(people);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching people from ufl_id: {ex.Message}");
            }
        }
    }
}
