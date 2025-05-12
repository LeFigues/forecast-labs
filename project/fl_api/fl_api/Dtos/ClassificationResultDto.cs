namespace fl_api.Dtos
{
    public class ClassificationResultDto
    {
        public Guid DocumentId { get; set; }
        public int CareerId { get; set; }
        public string CareerName { get; set; } = null!;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public DateTime ClassifiedAt { get; set; }
    }
}
