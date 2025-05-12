namespace fl_api.Interfaces
{
    public interface IPdfProcessingService
    {
        Task<string> ExtractToJsonAsync();
    }
}
