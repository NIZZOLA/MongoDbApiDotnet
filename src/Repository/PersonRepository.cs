using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApi.Data;
using MongoDbApi.Models;

namespace MongoDbApi.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly IMongoContext _context;
    public PersonRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PersonModel>> GetAll()
    {
        return await _context
                        .Persons
                        .Find(_ => true)
                        .ToListAsync();
    }
    public Task<PersonModel> GetOne(Guid id)
    {
        var filter = FindById(id);
        return _context
                .Persons
                .Find(filter)
                .FirstOrDefaultAsync();
    }
    public async Task Create(PersonModel person)
    {
        await _context.Persons.InsertOneAsync(person);
    }
    public async Task<bool> Update(PersonModel person)
    {
        ReplaceOneResult updateResult =
            await _context
                    .Persons
                    .ReplaceOneAsync(
                        filter: g => g.Id == person.Id,
                        replacement: person);
        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(Guid id)
    {
        var filter = FindById(id);
        DeleteResult deleteResult = await _context
                                            .Persons
                                          .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }

    private FilterDefinition<PersonModel> FindById(Guid id)
    {
        return Builders<PersonModel>.Filter.Eq(m => m.Id, id);
    }
    public async Task<Guid> GetNextId()
    {
        return Guid.NewGuid();
    }
}
