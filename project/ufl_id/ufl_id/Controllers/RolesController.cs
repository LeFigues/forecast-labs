using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Models;
using ufl_id.Services;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly RoleService _roleService;
        private readonly ILogger<RolesController> _logger;
        public RolesController(DataContext context, RoleService roleService, ILogger<RolesController> logger)
        {
            _context = context;
            _roleService = roleService;
            _logger = logger;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            try
            {
                var role = await _roleService.CreateRoleAsync(dto.RoleName);
                _logger.LogInformation("New role {RoleName} created successfully.", dto.RoleName);
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create role {RoleName}.", dto.RoleName);
                return BadRequest(ex.Message);
            }
        }

        // Asignar un rol a un usuario
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            try
            {
                await _roleService.AssignRoleAsync(dto.UserId, dto.RoleName);
                _logger.LogInformation("Role {RoleName} assigned to user with ID {UserId}.", dto.RoleName, dto.UserId);
                return Ok("Role assigned successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to assign role {RoleName} to user with ID {UserId}.", dto.RoleName, dto.UserId);
                return BadRequest(ex.Message);
            }
        }

        // Obtener roles de un usuario
        [HttpGet("{userId}/roles")]
        [Authorize]  // Solo usuarios autenticados
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            try
            {
                var roles = await _roleService.GetUserRolesAsync(userId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
