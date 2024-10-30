using MongoDB.Driver;
using Raven.Message.Domain.Entities;
using Raven.Message.Infrastructure;

namespace Raven.Message.Application.Handlers;

public class UserHandler(MongoContext context)
{
    private readonly IMongoCollection<UserProfile> _userProfiles = context.GetCollection<UserProfile>("UserProfiles");

    public async Task AddUserAsync(UserProfile user)
    {
        user.JoinedDate = DateTime.UtcNow;
        await _userProfiles.InsertOneAsync(user);
    }

    public async Task<UserProfile> GetUserByIdAsync(string userId)
    {
        return await _userProfiles
            .Find(user => user.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var deleteResult = await _userProfiles.DeleteOneAsync(user => user.UserId == userId);
        return deleteResult.DeletedCount > 0;
    }
    
    public async Task<bool> UpdateUserAsync(UserProfile updatedUser)
    {
        var result = await _userProfiles.ReplaceOneAsync(
            user => user.UserId == updatedUser.UserId,
            updatedUser                  
        );

        return result.ModifiedCount > 0;
    }
}