using MessageService.Application.Handlers;

namespace MessageService.Application.Services;

public class ChatService(UserChatHandler userChatHandler, ChatHandler chatHandler)
{
    public async Task<List<string>> GetChatList(string userId)
    {
        return await userChatHandler.GetChatsByUserId(userId);
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