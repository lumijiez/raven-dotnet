using MessageService.Domain.Entities;
using MessageService.Infrastructure;
using MongoDB.Driver;

namespace MessageService.Application.Handlers;

public class MessageHandler(MongoContext context)
{
    private readonly IMongoCollection<Message> _messages = context.GetCollection<Message>("Messages");
    
    public async Task AddMessage(Message newMessage)
    {
        await _messages.InsertOneAsync(newMessage);
    }
}