using System.Text.Json;

namespace fl_api.Interfaces
{
    public interface IOpenAIAnalysisService
    {
        Task<JsonDocument> AnalyzeAsync(Guid documentId, string? model = null);
    }
}
