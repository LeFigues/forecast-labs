using System.ComponentModel.DataAnnotations;

namespace fl_students_lib.Models
{
    public class GroupStudent
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int GroupId { get; set; }
        public SGroup? Group { get; set; }

    }
}
