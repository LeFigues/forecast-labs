using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassificationController : ControllerBase
    {
        private readonly IClassificationService _classification;

        public ClassificationController(IClassificationService classification)
            => _classification = classification;

        [HttpPost]
        public async Task<IActionResult> Classify([FromBody] ClassificationRequest req)
        {
            try
            {
                var res = await _classification.ClassifyAsync(req.DocumentId, req.SubjectName);
                return Ok(res);
            }
            catch (InvalidOperationException inv)
            {
                return BadRequest(new { message = inv.Message });
            }
        }
    }

    public class ClassificationRequest
    {
        public Guid DocumentId { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}
