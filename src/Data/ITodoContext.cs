using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Data;

public interface ITodoContext
{
    IMongoCollection<TodoModel> Todos { get; }
}
