using fl_front.Models;

namespace fl_front.Services
{
    public interface IDocumentService
    {
        Task<UploadResponse?> UploadPdfAsync(Stream fileStream, string fileName, int careerId, int subjectId);
        Task<AnalysisResponse?> AnalyzeWithOpenAIAsync(string documentId, string model = "gpt-4");
        Task ClassifyDocumentAsync(string documentId, string subjectName);

    }
}
