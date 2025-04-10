using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Models;
using ufl_id.Services;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, DataContext context, ILogger<AuthController> logger, TokenService tokenService)
        {
            _authService = authService;
            _context = context;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.ValidateUserCredentials(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            _logger.LogInformation("User {Username} logged in successfully.", user.Username);

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                ReplacedByToken = null // Asegúrate de que esté nulo si no reemplaza otro token
            });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Verificar si ya existe un usuario con el mismo nombre de usuario
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == registerDto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            // Crear una nueva persona y agregarla a la tabla People
            var person = new Person
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                CI = registerDto.CI,
                Birthdate = registerDto.Birthdate,
                PhoneNumber = registerDto.PhoneNumber
            };

            _context.People.Add(person);
            await _context.SaveChangesAsync(); // Guardar la persona para obtener su Id

            // Ahora crear el usuario con la referencia a la persona creada
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),  // Hashear la contraseña
                Status = 1,
                PersonId = person.Id,  // Asignar el PersonId de la persona recién creada
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();  // Guardar el usuario

            return Ok("User registered successfully");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var newRefreshToken = await _tokenService.RotateRefreshToken(refreshTokenDto.RefreshToken);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == refreshTokenDto.UserId);

                if (user == null)
                {
                    return Unauthorized("Invalid user");
                }

                var newAccessToken = _tokenService.GenerateAccessToken(user);

                return Ok(new
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }


}
