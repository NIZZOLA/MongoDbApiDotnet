using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbApi.Models;

namespace MongoDbApi.Data;

public class TodoContext : ITodoContext
{
    private readonly IMongoDatabase _db;
    public TodoContext(IOptions<MongoConfiguration> config)
    {
        var client = new MongoClient(config.Value.ConnectionString);
        _db = client.GetDatabase(config.Value.Database);
    }
    public IMongoCollection<TodoModel> Todos => _db.GetCollection<TodoModel>("Tarefas");
}