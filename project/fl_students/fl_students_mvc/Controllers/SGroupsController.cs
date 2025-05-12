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
    public class SGroupsController : Controller
    {
        private readonly DataContext _context;

        public SGroupsController(DataContext context)
        {
            _context = context;
        }

        // GET: SGroups
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.SGroups.Include(s => s.Semester).Include(s => s.Subject).Include(s => s.Teacher);
            return View(await dataContext.ToListAsync());
        }

        // GET: SGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sGroup = await _context.SGroups
                .Include(s => s.Semester)
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sGroup == null)
            {
                return NotFound();
            }

            return View(sGroup);
        }

        // GET: SGroups/Create
        public IActionResult Create()
        {
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.People.Include(s => s.User).ThenInclude(r => r.Role).Where(r => r.User.Role.Id == 2), "Id", "FirstName");
            return View();
        }

        // POST: SGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SemesterId,SubjectId,TeacherId")] SGroup sGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id", sGroup.SemesterId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", sGroup.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.People.Include(s => s.User).ThenInclude(r => r.Role).Where(r => r.User.Role.Id == 2), "Id", "FirstName", sGroup.TeacherId);
            return View(sGroup);
        }

        // GET: SGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sGroup = await _context.SGroups.FindAsync(id);
            if (sGroup == null)
            {
                return NotFound();
            }
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id", sGroup.SemesterId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", sGroup.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.People.Include(s => s.User).ThenInclude(r => r.Role).Where(r => r.User.Role.Id == 2), "Id", "FirstName", sGroup.TeacherId);
            return View(sGroup);
        }

        // POST: SGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SemesterId,SubjectId,TeacherId")] SGroup sGroup)
        {
            if (id != sGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SGroupExists(sGroup.Id))
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
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id", sGroup.SemesterId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", sGroup.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.People.Include(s => s.User).ThenInclude(r => r.Role).Where(r => r.User.Role.Id == 2), "Id", "FirstName", sGroup.TeacherId);
            return View(sGroup);
        }

        // GET: SGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sGroup = await _context.SGroups
                .Include(s => s.Semester)
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sGroup == null)
            {
                return NotFound();
            }

            return View(sGroup);
        }

        // POST: SGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sGroup = await _context.SGroups.FindAsync(id);
            if (sGroup != null)
            {
                _context.SGroups.Remove(sGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SGroupExists(int id)
        {
            return _context.SGroups.Any(e => e.Id == id);
        }
    }
}
