using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Career>? Careers { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
