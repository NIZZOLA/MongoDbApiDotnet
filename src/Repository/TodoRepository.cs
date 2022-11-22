using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApi.Data;
using MongoDbApi.Models;

namespace MongoDbApi.Repository;
public class TodoRepository : ITodoRepository
{
    private readonly IMongoContext _context;
    public TodoRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TodoModel>> GetAll()
    {
        return await _context
                        .Todos
                        .Find(_ => true)
                        .ToListAsync();
    }
    public Task<TodoModel> GetOne(long id)
    {
        var filter = FindById(id);
        return _context
                .Todos
                .Find(filter)
                .FirstOrDefaultAsync();
    }
    public async Task Create(TodoModel todo)
    {
        await _context.Todos.InsertOneAsync(todo);
    }
    public async Task<bool> Update(TodoModel todo)
    {
        ReplaceOneResult updateResult =
            await _context
                    .Todos
                    .ReplaceOneAsync(
                        filter: g => g.Id == todo.Id,
                        replacement: todo);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(long id)
    {
        var filter = FindById(id);
        DeleteResult deleteResult = await _context
                                            .Todos
                                          .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }

    private FilterDefinition<TodoModel> FindById(long id)
    {
        return Builders<TodoModel>.Filter.Eq(m => m.Id, id);
    }
    public async Task<int> GetNextId()
    {
        return (int) await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
    }
}
