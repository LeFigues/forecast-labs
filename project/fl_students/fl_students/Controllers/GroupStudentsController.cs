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
    public class GroupStudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public GroupStudentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/GroupStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupStudent>>> GetGroupStudents()
        {
            return await _context.GroupStudents.ToListAsync();
        }

        // GET: api/GroupStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupStudent>> GetGroupStudent(int id)
        {
            var groupStudent = await _context.GroupStudents.FindAsync(id);

            if (groupStudent == null)
            {
                return NotFound();
            }

            return groupStudent;
        }

        // PUT: api/GroupStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupStudent(int id, GroupStudent groupStudent)
        {
            if (id != groupStudent.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(groupStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupStudentExists(id))
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

        // POST: api/GroupStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupStudent>> PostGroupStudent(GroupStudent groupStudent)
        {
            _context.GroupStudents.Add(groupStudent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupStudentExists(groupStudent.GroupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGroupStudent", new { id = groupStudent.GroupId }, groupStudent);
        }

        // DELETE: api/GroupStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupStudent(int id)
        {
            var groupStudent = await _context.GroupStudents.FindAsync(id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            _context.GroupStudents.Remove(groupStudent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupStudentExists(int id)
        {
            return _context.GroupStudents.Any(e => e.GroupId == id);
        }
    }
}
