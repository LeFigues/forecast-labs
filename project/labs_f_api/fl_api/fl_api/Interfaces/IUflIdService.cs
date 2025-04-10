using System.Text.Json.Nodes;

namespace fl_api.Interfaces
{
    public interface IUflIdService
    {
        Task<JsonArray> GetPeopleAsync();
    }
}
