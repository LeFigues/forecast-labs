using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ApiToken>? ApiTokens { get; set; }
        public ICollection<UserActivity>? UserActivities { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }

}
