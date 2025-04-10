using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ufl_id.Data;
using ufl_id.Models;

namespace ufl_id.Services
{
    public class TokenService
    {
        private readonly DataContext _context;
        private readonly SymmetricSecurityKey _key;

        public TokenService(DataContext context, SymmetricSecurityKey key)
        {
            _context = context;
            _key = key;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://api.underflabs.com",  // Cambia a tu dominio
                audience: "https://api.underflabs.com", // Cambia a tu dominio
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RotateRefreshToken(string oldRefreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == oldRefreshToken && !rt.IsRevoked);

            if (token == null || token.Expiration < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }

            // Revocar el token actual
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;

            // Generar un nuevo refresh token
            var newRefreshToken = GenerateRefreshToken();
            token.ReplacedByToken = newRefreshToken;

            // Guardar el nuevo refresh token en la base de datos
            var refreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = token.UserId,
                Expiration = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return newRefreshToken;
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }

}
