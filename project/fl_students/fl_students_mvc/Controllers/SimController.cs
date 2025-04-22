using fl_students_lib.Data;
using fl_students_lib.DTOs;
using fl_students_lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace fl_students_mvc.Controllers
{
    public class SimController : Controller
    {
        private readonly DataContext _context;

        public SimController(DataContext context)
        {
            _context = context;
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            Random ran = new Random();
            var dataContext = _context.Students.Include(s=> s.Career).Include(s => s.Groups).Include(u => u.User).ThenInclude(u => u.Role);
            List<StudentDto> students = new List<StudentDto>();
            foreach (var item in dataContext)
            {
                
                StudentDto student = new StudentDto();
                student = new StudentDto
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    CI = item.CI,
                    Cellphone = item.Cellphone,
                    Address = item.Address,
                    Career = item.Career.Name,
                    Username = item.User.Username,
                    Role = item.User.Role.Name,
                    Semester = ran.Next(1,10).ToString(),
                };
                students.Add(student); 
            }
            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["CareerId"] = new SelectList(_context.Careers, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterStudentDto dto)
        {
            if (ModelState.IsValid)
            {
                // Crear el usuario
                var user = new User
                {
                    Username = dto.Username,
                    Password = dto.Password, // ⚠️ Idealmente usar hash aquí
                    Email = dto.Email,
                    RoleId = 3
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // Guardamos para que tenga un ID generado

                // Crear el estudiante con el ID del usuario recién creado
                var student = new Student
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    CI = dto.CI,
                    Cellphone = dto.Cellphone,
                    Address = dto.Address,
                    CareerId = dto.CareerId,
                    UserId = user.Id // Aquí enlazamos con el usuario
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CareerId"] = new SelectList(_context.Careers, "Id", "Name", dto.CareerId);
            return View(dto);
        }

    }
}
