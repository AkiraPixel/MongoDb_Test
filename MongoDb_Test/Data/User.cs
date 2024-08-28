using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_Test.Data
{
    public class User
    {
        [BsonId]
        public ObjectId UserId { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }

        public List<int> IntsList { get; set; } = new List<int>();
    }
}
