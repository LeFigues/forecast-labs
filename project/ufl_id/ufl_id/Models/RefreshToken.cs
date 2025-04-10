using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } // Campo para saber si el token fue revocado
        public DateTime? RevokedAt { get; set; } // Fecha en la que se revocó
        public string? ReplacedByToken { get; set; } // Token que reemplazó al actual
    }
}
