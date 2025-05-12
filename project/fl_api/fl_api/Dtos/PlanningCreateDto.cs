using System.Text.Json.Serialization;

namespace fl_api.Dtos
{
    public class PlanningCreateDto
    {

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; } = null!;

        [JsonPropertyName("classroom")]
        public string Classroom { get; set; } = null!;

        [JsonPropertyName("teacher")]
        public string Teacher { get; set; } = null!;

        [JsonPropertyName("labCode")]
        public string LabCode { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("grupos")]
        public int Groups { get; set; }

        [JsonPropertyName("students")]
        public List<StudentDto> Students { get; set; } = new();
    }
}
