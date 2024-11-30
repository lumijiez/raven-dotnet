using MongoDB.Driver;
using Raven.Message.Infrastructure;

namespace Raven.Message.Application.Handlers;

public class MessageHandler(MongoContext context)
{
    private readonly IMongoCollection<Domain.Entities.Message> _messages = context.GetCollection<Domain.Entities.Message>("Messages");
    
    public async Task AddMessage(Domain.Entities.Message newMessage)
    {
        await _messages.InsertOneAsync(newMessage);
    }
}