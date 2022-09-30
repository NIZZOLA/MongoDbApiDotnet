using MongoDbApi.Models;

namespace MongoDbApi.Repository;

public interface IPersonRepository
{
    Task<IEnumerable<PersonModel>> GetAll();
    Task<PersonModel> GetOne(Guid id);
    Task Create(PersonModel todo);
    Task<bool> Update(PersonModel todo);
    Task<bool> Delete(Guid id);
    Task<Guid> GetNextId();
}
