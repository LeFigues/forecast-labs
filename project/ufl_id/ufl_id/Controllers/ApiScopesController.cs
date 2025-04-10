using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.Models;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiScopesController : ControllerBase
    {
        private readonly DataContext _context;

        public ApiScopesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ApiScopes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiScope>>> GetApiScopes()
        {
            return await _context.ApiScopes.ToListAsync();
        }

        // GET: api/ApiScopes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiScope>> GetApiScope(int id)
        {
            var apiScope = await _context.ApiScopes.FindAsync(id);

            if (apiScope == null)
            {
                return NotFound();
            }

            return apiScope;
        }

        // PUT: api/ApiScopes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiScope(int id, ApiScope apiScope)
        {
            if (id != apiScope.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiScope).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiScopeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiScopes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiScope>> PostApiScope(ApiScope apiScope)
        {
            _context.ApiScopes.Add(apiScope);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApiScope", new { id = apiScope.Id }, apiScope);
        }

        // DELETE: api/ApiScopes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiScope(int id)
        {
            var apiScope = await _context.ApiScopes.FindAsync(id);
            if (apiScope == null)
            {
                return NotFound();
            }

            _context.ApiScopes.Remove(apiScope);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiScopeExists(int id)
        {
            return _context.ApiScopes.Any(e => e.Id == id);
        }
    }
}
