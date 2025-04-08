namespace fl_api.Interfaces
{
    public interface IStatusService
    {
        Task<bool> IsMongoConnectedAsync();
        Task<bool> IsGptApiAvailableAsync();
    }
}
