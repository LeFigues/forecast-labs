namespace fl_front.Models
{
    public class ForecastRequest
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Horizon { get; set; } = "monthly";
    }
}
