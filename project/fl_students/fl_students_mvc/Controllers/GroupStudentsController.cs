using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fl_students_lib.Data;
using fl_students_lib.Models;

namespace fl_students_mvc.Controllers
{
    public class GroupStudentsController : Controller
    {
        private readonly DataContext _context;

        public GroupStudentsController(DataContext context)
        {
            _context = context;
        }

        // GET: GroupStudents
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.GroupStudents.Include(g => g.Group).Include(g => g.Student);
            return View(await dataContext.ToListAsync());
        }

        // GET: GroupStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudents
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            return View(groupStudent);
        }

        // GET: GroupStudents/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.SGroups, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Address");
            return View();
        }

        // POST: GroupStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,StudentId,GroupId")] GroupStudent groupStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.SGroups, "Id", "Name", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Address", groupStudent.StudentId);
            return View(groupStudent);
        }

        // GET: GroupStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudents.FindAsync(id);
            if (groupStudent == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.SGroups, "Id", "Name", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Address", groupStudent.StudentId);
            return View(groupStudent);
        }

        // POST: GroupStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,StudentId,GroupId")] GroupStudent groupStudent)
        {
            if (id != groupStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupStudentExists(groupStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.SGroups, "Id", "Name", groupStudent.GroupId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Address", groupStudent.StudentId);
            return View(groupStudent);
        }

        // GET: GroupStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupStudent = await _context.GroupStudents
                .Include(g => g.Group)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupStudent == null)
            {
                return NotFound();
            }

            return View(groupStudent);
        }

        // POST: GroupStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupStudent = await _context.GroupStudents.FindAsync(id);
            if (groupStudent != null)
            {
                _context.GroupStudents.Remove(groupStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupStudentExists(int id)
        {
            return _context.GroupStudents.Any(e => e.Id == id);
        }
    }
}
