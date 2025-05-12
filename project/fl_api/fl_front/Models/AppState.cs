namespace fl_front.Models
{
    public class AppState
    {
        public UploadResponse? LastUpload { get; set; }
        public AnalysisResponse? LastAnalysis { get; set; }
        public HealthCheckResponse? LastHealthStatus { get; set; }

    }
}
