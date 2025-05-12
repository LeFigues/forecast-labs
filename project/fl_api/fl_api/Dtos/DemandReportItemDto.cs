namespace fl_api.Dtos
{
    public class DemandReportItemDto
    {
        public int CareerId { get; set; }
        public string CareerName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int TotalEquipmentItems { get; set; }
        public int TotalSupplyItems { get; set; }
        public int TotalReactiveItems { get; set; }
    }
}
