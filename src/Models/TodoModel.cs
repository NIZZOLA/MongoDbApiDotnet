using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbApi.Models;

public class TodoModel
{
    [BsonId]
    public ObjectId InternalId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
