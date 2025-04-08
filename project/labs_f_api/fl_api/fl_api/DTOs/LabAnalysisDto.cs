namespace fl_api.DTOs
{
    public class LabAnalysisDto
    {
        public string Laboratory { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Groups { get; set; }

        public MaterialsDto Materials { get; set; } = new();
    }
}
