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
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly DataContext _context;
        private readonly TokenService _tokenService;

        public AuthorizationController(AuthorizationService authorizationService, DataContext context, TokenService tokenService)
        {
            _authorizationService = authorizationService;
            _context = context;
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthorizationRequestDto authDto)
        {
            var client = await _context.ApiClients
                .FirstOrDefaultAsync(c => c.ClientId == authDto.ClientId && c.ClientSecret == authDto.ClientSecret);

            if (client == null)
            {
                return Unauthorized("Invalid client credentials");
            }

            var code = await _authorizationService.GenerateAuthorizationCode(authDto.UserId, client.Id);

            return Ok(new
            {
                AuthorizationCode = code.Code
            });
        }

        [HttpPost("token")]
        public async Task<IActionResult> ExchangeToken([FromBody] TokenExchangeDto tokenExchangeDto)
        {
            var authCode = await _authorizationService.ValidateAuthorizationCode(tokenExchangeDto.AuthorizationCode);

            if (authCode == null)
            {
                return Unauthorized("Invalid or expired authorization code");
            }

            var user = await _context.Users.FindAsync(authCode.UserId);
            var token = _tokenService.GenerateAccessToken(user);

            return Ok(new
            {
                AccessToken = token
            });
        }
    }

}
