namespace fl_api.Dtos
{
    public class PeriodForecastDto
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string Item { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public double ForecastQty { get; set; }
    }
}
