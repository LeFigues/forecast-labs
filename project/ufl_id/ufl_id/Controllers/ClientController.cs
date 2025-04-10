using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Models;
using ufl_id.Services;

namespace ufl_id.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;
        private readonly ILogger<ClientController> _logger;
        public ClientController(ClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        // Endpoint para registrar un cliente
        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientDto dto)
        {
            try
            {
                var client = await _clientService.RegisterClientAsync(dto);
                _logger.LogInformation("New client {ClientId} registered successfully.", client.ClientId);
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register client {ClientId}.", dto.ClientId);
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para validar un cliente
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateClient([FromBody] ClientDto clientDto)
        {
            var client = await _clientService.ValidateClientCredentials(clientDto.ClientId, clientDto.ClientSecret);

            if (client == null)
            {
                return Unauthorized("Invalid client credentials");
            }

            return Ok(client);
        }
    }


}
