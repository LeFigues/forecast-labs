namespace fl_students_lib.Models
{
    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public int CareerId { get; set; }
        public Career? Career { get; set; }

        public ICollection<GroupStudent>? Groups { get; set; }
    }
}
