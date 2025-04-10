namespace ufl_id.DTO
{
    public class RegisterClientDto
    {
        public string ClientId { get; set; }     // Identificador único del cliente (ej: 'my_client_app')
        public string ClientSecret { get; set; } // Secreto para autenticar el cliente
        public string RedirectUri { get; set; }  // URI de redirección para OAuth
        public string Name { get; set; }         // Nombre de la aplicación cliente
    }
}
