namespace fl_front.Models
{
    public class AnalysisResponse
    {
        public string Id { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; }
        public int PracticeNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Groups { get; set; }
        public AnalysisResult? AnalysisResult { get; set; } = new();
        public string ModelUsed { get; set; } = string.Empty;
        public DateTime AnalyzedAt { get; set; }
        public int SatisfactionPercentage { get; set; }
    }
}
