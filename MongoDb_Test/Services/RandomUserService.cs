using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDb_Test.Data;

namespace MongoDb_Test.Services
{
    public class RandomUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public RandomUserService(IOptions<MongoDbDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(
                options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                options.Value.UserCollectionName);
        }


        public async Task CreateManyUsers(int count)
        {
            await _usersCollection.InsertManyAsync(GetManyUser(count));
        }


        private IEnumerable<User> GetManyUser(int count)
        {
            return Enumerable.Range(1, count)
                .AsParallel()
                .Select(index => CreateUser(index));

        }

        private User CreateUser(int index)
        {
            var item = new User()
            {
                Guid = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index * -1),
                //IntsList = new List<int>(index),
                IntsList = Enumerable.Range(1, index).ToList(),
                Name = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            //item.IntsList.AddRange(Enumerable.Range(1, index));

            return item;
        }


    }
}
