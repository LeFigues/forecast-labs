using System.Text.Json;

namespace fl_api.Dtos
{
    public class PlanningResponseDto
    {
        public string Id { get; set; } = null!;
        public Guid DocumentId { get; set; }
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
        public JsonElement AnalysisResult { get; set; }  // <-- usar JsonElement
        public DateTime CreatedAt { get; set; }
    }

}
