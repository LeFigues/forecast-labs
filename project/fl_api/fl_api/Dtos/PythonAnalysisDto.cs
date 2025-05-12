using System.Text.Json;

namespace fl_api.Dtos
{
    public class PythonAnalysisDto
    {
        public Guid DocumentId { get; set; }
        public string FileName { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Version { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public int PracticeNumber { get; set; }
        public string Title { get; set; } = null!;
        public int Groups { get; set; }
        public JsonElement Materials { get; set; }
        public JsonElement AnalysisResult { get; set; }
        public string ModelUsed { get; set; } = null!;
        public DateTime AnalyzedAt { get; set; }
        public double SatisfactionPercentage { get; set; }
    }

}
