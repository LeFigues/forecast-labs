namespace fl_front.Models
{
    public class SimulationItem
    {
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int TotalDemand { get; set; }
        public double AverageDailyUse { get; set; }
        public int RecommendedOrder { get; set; }
    }
}
