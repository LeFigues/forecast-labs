namespace fl_api.Dtos
{
    public class UploadDocumentDto
    {
        public IFormFile File { get; set; } = null!;
        public int CareerId { get; set; }
        public int SubjectId { get; set; }
    }
}
