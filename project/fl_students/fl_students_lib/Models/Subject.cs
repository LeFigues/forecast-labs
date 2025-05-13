using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public int CareerId { get; set; }
        public Career? Career { get; set; }
        public ICollection<SGroup>? Groups { get; set; }

    }
}
