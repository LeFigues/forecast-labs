namespace fl_api.Dtos
{
    public class DailyDemandDto
    {
        public DateTime Date { get; set; }
        public string Item { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
