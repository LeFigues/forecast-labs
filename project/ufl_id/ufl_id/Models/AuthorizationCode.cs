using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class AuthorizationCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ApiClientId { get; set; }
        public ApiClient ApiClient { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsUsed { get; set; }
    }
}
