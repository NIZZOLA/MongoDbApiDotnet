using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Data;

public interface IMongoContext
{
    IMongoCollection<TodoModel> Todos { get; }
    IMongoCollection<PersonModel> Persons { get; }
}
