using MongoDbApi.Models;

namespace MongoDbApi.Repository;

public interface ITodoRepository
{
    Task<IEnumerable<TodoModel>> GetAll();
    Task<TodoModel> GetOne(long id);
    Task Create(TodoModel todo);
    Task<bool> Update(TodoModel todo);
    Task<bool> Delete(long id);
    Task<long> GetNextId();
}
