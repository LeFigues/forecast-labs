using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Services;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenValidationService _tokenValidationService;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;


        public TokenController(TokenValidationService tokenValidationService, TokenService tokenService, DataContext context)
        {
            _tokenValidationService = tokenValidationService;
            _tokenService = tokenService;
            _context = context;
        }
        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var refreshToken = await _tokenValidationService.ValidateRefreshToken(tokenDto.RefreshToken);

            if (refreshToken == null)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            // Generar un nuevo access token
            var user = await _context.Users.FindAsync(refreshToken.UserId);
            var newAccessToken = _tokenService.GenerateAccessToken(user);

            return Ok(new
            {
                AccessToken = newAccessToken
            });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] TokenDto tokenDto)
        {
            var result = await _tokenValidationService.RevokeRefreshToken(tokenDto.RefreshToken);

            if (!result)
            {
                return NotFound("Token not found");
            }

            return Ok("Token revoked");
        }
    }

}
