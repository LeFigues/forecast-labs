namespace fl_front.Models
{
    public class DemandDetail
    {
        public int CareerId { get; set; }
        public string CareerName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public List<ReportItem> Equipment { get; set; } = new();
        public List<ReportItem> Supplies { get; set; } = new();
        public List<ReportItem> Reactives { get; set; } = new();
    }
}
