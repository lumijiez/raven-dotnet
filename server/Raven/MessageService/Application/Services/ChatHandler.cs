using MessageService.Domain.Entities;
using MessageService.Infrastructure;
using MongoDB.Driver;

namespace MessageService.Application.Services;

public class ChatHandler(MongoContext context)
{
    private readonly IMongoCollection<Chat> _chats = context.GetCollection<Chat>("Chats");
    
    public async Task AddUser(int userId, int chatId)
    {
        var chat = await _chats.Find(c => c.ChatId == chatId.ToString()).FirstOrDefaultAsync();

        if (chat != null)
        {
            if (!chat.Participants.Contains(userId.ToString()))
            {
                chat.Participants.Add(userId.ToString());
                await _chats.ReplaceOneAsync(c => c.ChatId == chatId.ToString(), chat);
            }
        }
        else
        {
            var newChat = new Chat
            {
                ChatId = chatId.ToString(),
                Participants = [userId.ToString()],
                LastMessageTimestamp = DateTime.UtcNow
            };
            await _chats.InsertOneAsync(newChat);
        }
    }

}