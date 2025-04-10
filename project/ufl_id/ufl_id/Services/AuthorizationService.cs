using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.Models;

namespace ufl_id.Services
{
    public class AuthorizationService
    {
        private readonly DataContext _context;

        public AuthorizationService(DataContext context)
        {
            _context = context;
        }

        // Generar un código de autorización
        public async Task<AuthorizationCode> GenerateAuthorizationCode(int userId, int apiClientId)
        {
            var code = new AuthorizationCode
            {
                Code = Guid.NewGuid().ToString(),
                UserId = userId,
                ApiClientId = apiClientId,
                Expiration = DateTime.UtcNow.AddMinutes(10) // Expira en 10 minutos
            };

            _context.AuthorizationCodes.Add(code);
            await _context.SaveChangesAsync();

            return code;
        }

        // Validar el código de autorización
        public async Task<AuthorizationCode?> ValidateAuthorizationCode(string authorizationCode)
        {
            var code = await _context.AuthorizationCodes
                .FirstOrDefaultAsync(c => c.Code == authorizationCode && c.Expiration > DateTime.UtcNow);

            return code;
        }
    }
}
