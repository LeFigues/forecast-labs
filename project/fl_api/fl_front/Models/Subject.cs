namespace fl_front.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CareerId { get; set; }
    }
}
