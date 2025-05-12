namespace fl_api.Dtos
{
    public class DemandReportDetailDto
    {
        public int CareerId { get; set; }
        public string CareerName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;

        public List<ItemDto> Equipment { get; set; } = new();
        public List<ItemDto> Supplies { get; set; } = new();
        public List<ItemDto> Reactives { get; set; } = new();
    }
}
