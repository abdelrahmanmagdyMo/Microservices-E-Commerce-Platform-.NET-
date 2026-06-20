using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public class BaseEntity
    {
        [BsonId] // definition of pk
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] // mapping between document in db and user presentation
        public string Id { get; set; }
    }
}
