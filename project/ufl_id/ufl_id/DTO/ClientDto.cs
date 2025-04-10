namespace ufl_id.DTO
{
    public class ClientDto
    {
        public string ClientId { get; set; } // Se usará al validar un cliente
        public string ClientSecret { get; set; } // Se usará al validar un cliente
        public string Name { get; set; } // Para registrar un cliente nuevo
        public string RedirectUri { get; set; } // Para registrar un cliente nuevo
    }


}
