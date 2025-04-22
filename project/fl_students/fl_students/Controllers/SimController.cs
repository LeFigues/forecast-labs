using fl_students_lib.Data;
using fl_students_lib.DTOs;
using fl_students_lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fl_students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimController : ControllerBase
    {
        private readonly DataContext _context;

        public SimController(DataContext context)
        {
            _context = context;
        }

    }
}
