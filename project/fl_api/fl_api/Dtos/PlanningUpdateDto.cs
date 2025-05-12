namespace fl_api.Dtos
{
    public class PlanningUpdateDto
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Group { get; set; } = null!;
        public string Classroom { get; set; } = null!;
        public string Teacher { get; set; } = null!;
        public string LabCode { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Groups { get; set; }
        public List<StudentDto> Students { get; set; } = new();
    }
}
