namespace fl_front.Models
{
    public class DemandSimulation
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CoverageDays { get; set; }
        public int LeadTimeDays { get; set; }
        public List<SimulationItem> Items { get; set; } = new();
    }
}
