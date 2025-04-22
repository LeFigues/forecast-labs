using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}
