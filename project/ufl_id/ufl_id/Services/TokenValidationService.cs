namespace ufl_id.Services
{
    using Microsoft.EntityFrameworkCore;
    using ufl_id.Data;
    using ufl_id.Models;

    public class TokenValidationService
    {
        private readonly DataContext _context;

        public TokenValidationService(DataContext context)
        {
            _context = context;
        }

        // Método para validar el Refresh Token
        public async Task<RefreshToken?> ValidateRefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken && !t.IsRevoked && t.Expiration > DateTime.UtcNow);

            return token;
        }

        // Método para revocar un Refresh Token
        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);

            if (token == null)
            {
                return false; // Token no encontrado
            }

            token.IsRevoked = true; // Marcar el token como revocado
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
