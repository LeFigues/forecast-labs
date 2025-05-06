using fl_api.Interfaces;
using fl_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplyReportsController : ControllerBase
    {
        private readonly ISupplyReportService _service;

        public SupplyReportsController(ISupplyReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyReport>>> GetAll() =>
            Ok(await _service.GetAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyReport>> Get(string id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplyReport report)
        {
            await _service.CreateAsync(report);
            return CreatedAtAction(nameof(Get), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, SupplyReport report)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            report.Id = id;
            await _service.UpdateAsync(id, report);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
