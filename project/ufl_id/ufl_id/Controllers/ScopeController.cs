using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Models;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopeController : ControllerBase
    {
        private readonly DataContext _context;

        public ScopeController(DataContext context)
        {
            _context = context;
        }

        // Endpoint para crear un nuevo scope
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateScope([FromBody] ScopeDto scopeDto)
        {
            var scope = new ApiScope
            {
                ScopeName = scopeDto.ScopeName,
                Description = scopeDto.Description
            };

            _context.ApiScopes.Add(scope);
            await _context.SaveChangesAsync();

            return Ok(scope);
        }

        // Endpoint para obtener todos los scopes
        [HttpGet]
        public async Task<IActionResult> GetScopes()
        {
            var scopes = await _context.ApiScopes.ToListAsync();
            return Ok(scopes);
        }

        // Endpoint para eliminar un scope
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScope(int id)
        {
            var scope = await _context.ApiScopes.FindAsync(id);

            if (scope == null)
            {
                return NotFound("Scope not found");
            }

            _context.ApiScopes.Remove(scope);
            await _context.SaveChangesAsync();

            return Ok("Scope deleted");
        }
    }

}
