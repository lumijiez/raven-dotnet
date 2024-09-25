using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MessageService.Domain.Entities;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Text")]
    public string Text { get; set; }

    [BsonElement("ReceiverId")]
    public string ReceiverId { get; set; }

    [BsonElement("ChatId")]
    public string ChatId { get; set; }

    public DateTime Timestamp { get; set; }
}
