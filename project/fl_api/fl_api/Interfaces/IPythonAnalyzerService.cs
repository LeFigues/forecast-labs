using System.Text.Json;

namespace fl_api.Interfaces
{
    public interface IPythonAnalyzerService
    {
        Task<JsonDocument> AnalyzeWithPythonAsync(Guid documentId);
    }
}
