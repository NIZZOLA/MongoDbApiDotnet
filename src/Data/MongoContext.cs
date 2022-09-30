using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Data;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _db;
    public MongoContext(IOptions<MongoConfiguration> config)
    {
        var client = new MongoClient(config.Value.ConnectionString);
        _db = client.GetDatabase(config.Value.Database);
    }
    public IMongoCollection<TodoModel> Todos => _db.GetCollection<TodoModel>("Tarefas");
    public IMongoCollection<PersonModel> Persons => _db.GetCollection<PersonModel>("Persons");
}