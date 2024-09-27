using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Raven.Message.Domain.Entities;

namespace Raven.Message.Infrastructure;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        
        var indexKeysDefinition = Builders<UserProfile>.IndexKeys.Ascending(userProfile => userProfile.UserId);
        var indexModel = new CreateIndexModel<UserProfile>(indexKeysDefinition, new CreateIndexOptions { Unique = true });
        
        _database.GetCollection<UserProfile>("UserProfiles").Indexes.CreateOneAsync(indexModel);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}