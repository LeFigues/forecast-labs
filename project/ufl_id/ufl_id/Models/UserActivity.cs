using System.ComponentModel.DataAnnotations;

namespace ufl_id.Models
{
    public class UserActivity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime ActivityTime { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
    }

}
