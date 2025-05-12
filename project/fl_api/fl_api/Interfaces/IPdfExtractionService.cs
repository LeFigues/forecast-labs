using System.Text.Json;

namespace fl_api.Interfaces
{
    public interface IPdfExtractionService
    {
        Task<JsonDocument> ExtractJsonAsync(Guid documentId);
    }
}
