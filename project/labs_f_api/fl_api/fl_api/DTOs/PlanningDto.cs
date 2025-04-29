namespace fl_api.DTOs
{
    public class PlanningDto
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Career { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Classroom { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;

        public List<StudentDto> Students { get; set; } = new();
        public List<MaterialDto> Materials { get; set; } = new();
    }
}
