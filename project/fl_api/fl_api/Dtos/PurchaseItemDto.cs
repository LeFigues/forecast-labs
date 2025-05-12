namespace fl_api.Dtos
{
    public class PurchaseItemDto
    {
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public int TotalDemand { get; set; }
        public double AverageDailyUse { get; set; }
        public int RecommendedOrder { get; set; }
    }
}
