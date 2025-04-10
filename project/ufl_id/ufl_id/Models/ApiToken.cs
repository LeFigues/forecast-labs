using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class ApiToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
