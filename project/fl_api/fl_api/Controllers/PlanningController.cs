using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanningController : ControllerBase
    {
        private readonly IPlanningService _svc;
        public PlanningController(IPlanningService svc) => _svc = svc;

        // GET /api/planning
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recs = await _svc.GetAllPlanningsAsync();

            var dtos = recs.Select(rec => ToDto(rec)).ToList();
            return Ok(dtos);
        }

        // GET /api/planning/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var rec = await _svc.GetPlanningAsync(id);
            if (rec == null) return NotFound();

            return Ok(ToDto(rec));
        }

        // POST /api/planning
        [HttpPost("{documentId:guid}")]
        public async Task<IActionResult> Create(Guid documentId, [FromBody] PlanningCreateDto dto)
        {
            var rec = await _svc.CreatePlanningAsync(documentId, dto);
            var response = ToDto(rec);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        // PUT /api/planning/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PlanningUpdateDto dto)
        {
            var updated = await _svc.UpdatePlanningAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE /api/planning/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _svc.DeletePlanningAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }


        private PlanningResponseDto ToDto(fl_api.Models.PlanningRecord rec)
        {
            // Convertir el BsonDocument a JSON string
            var analysisJson = rec.AnalysisResult.ToJson();
            // Parsear ese JSON a JsonElement
            var analysisElement = JsonDocument.Parse(analysisJson).RootElement;

            return new PlanningResponseDto
            {
                Id = rec.Id.ToString(),
                DocumentId = rec.DocumentId,
                Date = rec.Date,
                StartTime = rec.StartTime,
                EndTime = rec.EndTime,
                Group = rec.Group,
                Classroom = rec.Classroom,
                Teacher = rec.Teacher,
                LabCode = rec.LabCode,
                Title = rec.Title,
                Groups = rec.Groups,
                Students = rec.Students
                                        .Select(s => new StudentDto
                                        {
                                            FirstName = s.FirstName,
                                            LastName = s.LastName
                                        })
                                        .ToList(),
                AnalysisResult = analysisElement,
                CreatedAt = rec.CreatedAt
            };
        }
    }
}
