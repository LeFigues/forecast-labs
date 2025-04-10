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
    public class ApiTokensController : ControllerBase
    {
        private readonly DataContext _context;

        public ApiTokensController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ApiTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiToken>>> GetApiTokens()
        {
            return await _context.ApiTokens.ToListAsync();
        }

        // GET: api/ApiTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiToken>> GetApiToken(int id)
        {
            var apiToken = await _context.ApiTokens.FindAsync(id);

            if (apiToken == null)
            {
                return NotFound();
            }

            return apiToken;
        }

        // PUT: api/ApiTokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiToken(int id, ApiToken apiToken)
        {
            if (id != apiToken.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiTokenExists(id))
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

        // POST: api/ApiTokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiToken>> PostApiToken(ApiToken apiToken)
        {
            _context.ApiTokens.Add(apiToken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApiToken", new { id = apiToken.Id }, apiToken);
        }

        // DELETE: api/ApiTokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiToken(int id)
        {
            var apiToken = await _context.ApiTokens.FindAsync(id);
            if (apiToken == null)
            {
                return NotFound();
            }

            _context.ApiTokens.Remove(apiToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiTokenExists(int id)
        {
            return _context.ApiTokens.Any(e => e.Id == id);
        }
    }
}
