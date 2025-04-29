using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fl_students_lib.Data;
using fl_students_lib.Models;

namespace fl_students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SGroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public SGroupsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SGroup>>> GetSGroups()
        {
            return await _context.SGroups.ToListAsync();
        }

        // GET: api/SGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SGroup>> GetSGroup(int id)
        {
            var sGroup = await _context.SGroups.Include(s => s.Schedule).Include(s => s.Students).ThenInclude(s => s.Student).Where(s => s.Id == id).FirstOrDefaultAsync();

            if (sGroup == null)
            {
                return NotFound();
            }

            return sGroup;
        }
        [HttpGet("by-subject/{subjectId}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsByCareer(int subjectId)
        {
            var groups = await _context.SGroups
                .Where(s => s.SubjectId == subjectId)
                .Include(s => s.Students) // Si quieres incluir grupos relacionados
                .ToListAsync();

            if (groups == null || !groups.Any())
            {
                return NotFound();
            }

            return Ok(groups);
        }

        // PUT: api/SGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSGroup(int id, SGroup sGroup)
        {
            if (id != sGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(sGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SGroupExists(id))
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

        // POST: api/SGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SGroup>> PostSGroup(SGroup sGroup)
        {
            _context.SGroups.Add(sGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSGroup", new { id = sGroup.Id }, sGroup);
        }

        // DELETE: api/SGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSGroup(int id)
        {
            var sGroup = await _context.SGroups.FindAsync(id);
            if (sGroup == null)
            {
                return NotFound();
            }

            _context.SGroups.Remove(sGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SGroupExists(int id)
        {
            return _context.SGroups.Any(e => e.Id == id);
        }
    }
}
