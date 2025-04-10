
using Microsoft.EntityFrameworkCore;
using ufl_id.Data;
using ufl_id.DTO;
using ufl_id.Models;
namespace ufl_id.Services
{

    public class ClientService
    {
        private readonly DataContext _context;

        public ClientService(DataContext context)
        {
            _context = context;
        }

        // Registrar un nuevo cliente
        public async Task<ApiClient> RegisterClient(string name, string redirectUri)
        {
            var client = new ApiClient
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = Guid.NewGuid().ToString(),
                RedirectUri = redirectUri,
                Name = name
            };

            _context.ApiClients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }
        public async Task<ApiClient> RegisterClientAsync(RegisterClientDto dto)
        {
            // Verificar si ya existe un cliente con el mismo ClientId
            var existingClient = await _context.ApiClients.FirstOrDefaultAsync(c => c.ClientId == dto.ClientId);
            if (existingClient != null)
            {
                throw new Exception($"A client with ClientId '{dto.ClientId}' already exists.");
            }

            // Crear el nuevo cliente
            var client = new ApiClient
            {
                ClientId = dto.ClientId,
                ClientSecret = dto.ClientSecret,
                RedirectUri = dto.RedirectUri,
                Name = dto.Name
            };

            // Guardar el cliente en la base de datos
            _context.ApiClients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }
        // Validar las credenciales del cliente
        public async Task<ApiClient?> ValidateClientCredentials(string clientId, string clientSecret)
        {
            return await _context.ApiClients
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.ClientSecret == clientSecret);
        }
    }

}
