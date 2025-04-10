using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.Models;

namespace ufl_id.Services
{
    public class RoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRoleAsync(string roleName)
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (existingRole != null)
                throw new Exception("Role already exists");

            var role = new Role { Name = roleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task AssignRoleAsync(int userId, string roleName)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
                throw new Exception("Role not found");

            var userRole = new UserRole { UserId = userId, RoleId = role.Id };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var roles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            return roles;
        }
    }

}
