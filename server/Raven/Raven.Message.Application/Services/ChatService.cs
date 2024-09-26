using Raven.Message.Application.Handlers;
using Raven.Message.Domain.Entities;

namespace Raven.Message.Application.Services;

public class ChatService(UserChatHandler userChatHandler, ChatHandler chatHandler)
{
    public async Task<List<Chat>> GetChatsAsync(string userId)
    {
        var chatList = await userChatHandler.GetChatsByUserId(userId);
        var chatDetails = new List<Chat>();

        foreach (var chatId in chatList)
        {
            var chat = await chatHandler.GetChatDetailsAsync(chatId);
            chatDetails.Add(chat);
        }

        return chatDetails; 
    }

    public async Task<string> CreateSimpleChatAsync(string userOne, string userTwo)
    {
        var chatId = await chatHandler.CreateSimpleChatAsync(userOne, userTwo);
        await userChatHandler.BindUserToChatAsync(userOne, chatId);
        await userChatHandler.BindUserToChatAsync(userTwo, chatId);
        return chatId;
    }

    public async Task AddUserToChatAsync(string chatId, string userId)
    {
        await userChatHandler.BindUserToChatAsync(userId, chatId);
        await chatHandler.AddUserAsync(userId, chatId);
    }
}