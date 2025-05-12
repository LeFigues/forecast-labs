namespace fl_front.Models
{
    public class DemandHistoryItem
    {
        public DateTime Date { get; set; }
        public string Item { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
