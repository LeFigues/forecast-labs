using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }

        public ICollection<SGroup>? Groups { get; set; }
    }
}
