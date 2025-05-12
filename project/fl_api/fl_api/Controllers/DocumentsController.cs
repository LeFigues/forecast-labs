using fl_api.Dtos;
using fl_api.Interfaces;
using fl_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _docService;
        private readonly IMongoDbService _mongo;

        public DocumentsController(IDocumentService docService, IMongoDbService mongo)
        {
            _docService = docService;
            _mongo = mongo;
        }

        [HttpPost("send-pdf")]
        [RequestSizeLimit(52428800)] // 50MB
        public async Task<ActionResult<DocumentRecord>> Upload([FromForm] UploadDocumentDto dto)
        {
            if (!dto.File.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only PDF files are allowed.");

            var record = await _docService.SaveDocumentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentRecord>> GetById(Guid id)
        {
            // Use injected IMongoDbService instead of concrete DocumentService
            var collection = _mongo.GetCollection<DocumentRecord>("Documents");
            var doc = await collection.Find(d => d.Id == id).FirstOrDefaultAsync();
            if (doc == null)
                return NotFound();
            return Ok(doc);
        }
    }
}
