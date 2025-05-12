namespace fl_api.Dtos
{
    public class PurchaseSimulationDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int CoverageDays { get; set; }
        public int LeadTimeDays { get; set; }

        public List<PurchaseItemDto> Items { get; set; } = new();
    }
}
