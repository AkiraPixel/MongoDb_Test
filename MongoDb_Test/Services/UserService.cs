using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Test.Data;

namespace MongoDb_Test.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _booksCollection;

        public UserService(IOptions<MongoDbDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(
                options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<User>(
                options.Value.UserCollectionName);
        }


        public async Task<List<User>> GetAsync() =>
            await _booksCollection.Find(new BsonDocument()).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.UserId.ToString() == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, User updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.UserId.ToString() == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.UserId.ToString() == id);
    }
}
