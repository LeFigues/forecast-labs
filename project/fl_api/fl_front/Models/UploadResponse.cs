namespace fl_front.Models
{
    public class UploadResponse
    {
        public string Id { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public int CareerId { get; set; }
        public string CareerName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
    }
}
