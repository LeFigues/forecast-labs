namespace fl_front.Models
{
    public class AnalysisResult
    {
        public int Groups { get; set; }
        public MaterialList? Materials { get; set; } = new();
        public int? CreditsUsed { get; set; }
    }
}
