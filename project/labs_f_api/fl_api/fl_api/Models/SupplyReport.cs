namespace fl_api.Models
{
    public class SupplyReport
    {
        public string Name { get; set; } = string.Empty;
        public int QuantityUsed { get; set; }
        public int QuantityDamaged { get; set; }
        public int QuantityConsumed { get; set; }
        public int QuantityRequired { get; set; }
    }
}
