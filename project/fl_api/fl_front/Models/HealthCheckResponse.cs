namespace fl_front.Models
{
    public class HealthCheckResponse
    {
        public string OverallStatus { get; set; } = string.Empty;
        public Dictionary<string, HealthCheckItem> Checks { get; set; } = new();
    }
}
