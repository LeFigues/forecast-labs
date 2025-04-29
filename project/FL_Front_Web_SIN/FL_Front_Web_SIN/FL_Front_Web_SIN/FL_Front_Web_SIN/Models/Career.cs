// Models/Career.cs
namespace FL_Front_Web_SIN.Models
{
    public class Career
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Subject> Subjects { get; set; } = new();
    }
}