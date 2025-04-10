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
    public class AuthorizationCodesController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorizationCodesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AuthorizationCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorizationCode>>> GetAuthorizationCodes()
        {
            return await _context.AuthorizationCodes.ToListAsync();
        }

        // GET: api/AuthorizationCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorizationCode>> GetAuthorizationCode(int id)
        {
            var authorizationCode = await _context.AuthorizationCodes.FindAsync(id);

            if (authorizationCode == null)
            {
                return NotFound();
            }

            return authorizationCode;
        }

        // PUT: api/AuthorizationCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorizationCode(int id, AuthorizationCode authorizationCode)
        {
            if (id != authorizationCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(authorizationCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationCodeExists(id))
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

        // POST: api/AuthorizationCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorizationCode>> PostAuthorizationCode(AuthorizationCode authorizationCode)
        {
            _context.AuthorizationCodes.Add(authorizationCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorizationCode", new { id = authorizationCode.Id }, authorizationCode);
        }

        // DELETE: api/AuthorizationCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorizationCode(int id)
        {
            var authorizationCode = await _context.AuthorizationCodes.FindAsync(id);
            if (authorizationCode == null)
            {
                return NotFound();
            }

            _context.AuthorizationCodes.Remove(authorizationCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorizationCodeExists(int id)
        {
            return _context.AuthorizationCodes.Any(e => e.Id == id);
        }
    }
}
