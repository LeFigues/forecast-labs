namespace fl_api.Dtos
{
    public class ForecastRequestDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        /// <summary>
        /// "monthly" o "semestral"
        /// </summary>
        public string Horizon { get; set; } = "monthly";
    }
}
