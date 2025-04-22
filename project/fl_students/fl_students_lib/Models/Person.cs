using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
