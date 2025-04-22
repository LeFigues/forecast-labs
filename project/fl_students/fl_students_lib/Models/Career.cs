using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Career
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }
}
