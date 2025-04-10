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
    public class ApiClientsController : ControllerBase
    {
        private readonly DataContext _context;

        public ApiClientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ApiClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiClient>>> GetApiClients()
        {
            return await _context.ApiClients.ToListAsync();
        }

        // GET: api/ApiClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiClient>> GetApiClient(int id)
        {
            var apiClient = await _context.ApiClients.FindAsync(id);

            if (apiClient == null)
            {
                return NotFound();
            }

            return apiClient;
        }

        // PUT: api/ApiClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiClient(int id, ApiClient apiClient)
        {
            if (id != apiClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiClientExists(id))
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

        // POST: api/ApiClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiClient>> PostApiClient(ApiClient apiClient)
        {
            _context.ApiClients.Add(apiClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApiClient", new { id = apiClient.Id }, apiClient);
        }

        // DELETE: api/ApiClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiClient(int id)
        {
            var apiClient = await _context.ApiClients.FindAsync(id);
            if (apiClient == null)
            {
                return NotFound();
            }

            _context.ApiClients.Remove(apiClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiClientExists(int id)
        {
            return _context.ApiClients.Any(e => e.Id == id);
        }
    }
}
