using fl_api.Configurations;
using fl_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "prompts.json");
        private readonly PromptSettings _settings;

        public PromptController(IOptions<PromptSettings> options)
        {
            _settings = options.Value;
        }

        // GET: api/prompt
        [HttpGet]
        public IActionResult GetAll()
        {
            var prompts = ReadPrompts();
            return Ok(prompts);
        }

        // GET: api/prompt/correction
        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var prompts = ReadPrompts();
            var prompt = prompts.FirstOrDefault(p => p.Key == key);
            return prompt is null ? NotFound() : Ok(prompt);
        }

        // PUT: api/prompt/correction
        [HttpPut("{key}")]
        public IActionResult Update(string key, [FromBody] string newText)
        {
            var prompts = ReadPrompts();
            var prompt = prompts.FirstOrDefault(p => p.Key == key);

            if (prompt == null)
                return NotFound();

            prompt.Text = newText;
            SavePrompts(prompts);
            return Ok(prompt);
        }

        private List<PromptDefinition> ReadPrompts()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<PromptDefinition>();

            var json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<PromptDefinition>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<PromptDefinition>();
        }

        private void SavePrompts(List<PromptDefinition> prompts)
        {
            var json = JsonSerializer.Serialize(prompts, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            System.IO.File.WriteAllText(_filePath, json);
        }
    }
}
