using Raven.Message.Application.Handlers;
using Raven.Message.Domain.Entities;

namespace Raven.Message.Application.Services;

public class UserService(UserHandler userHandler)
{
    public async Task<UserProfile> AddUserAsync(UserProfile profile)
    {
        await userHandler.AddUserAsync(profile);
        return profile;
    }
    
    public async Task<UserProfile> GetUserByIdAsync(string userId)
    {
        return await userHandler.GetUserByIdAsync(userId);
    }
    
    // To Do
    // public async Task<UserProfile> UpdateUserAsync(UserProfile profile)
    // {
    //     var existingUser = await userHandler.GetUserByIdAsync(userId);
    //
    //     existingUser.DisplayName = displayName;
    //     existingUser.ProfilePictureUrl = profilePictureUrl;
    //     existingUser.Bio = bio;
    //
    //     await userHandler.UpdateUserAsync(userId, existingUser);
    //     return existingUser;
    // }
    
    public async Task<bool> DeleteUserAsync(string userId)
    {
        return await userHandler.DeleteUserAsync(userId);
    }
}