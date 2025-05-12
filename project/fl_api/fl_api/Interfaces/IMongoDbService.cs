using MongoDB.Driver;

namespace fl_api.Interfaces
{
    public interface IMongoDbService
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
