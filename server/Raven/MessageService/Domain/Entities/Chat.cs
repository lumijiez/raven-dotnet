namespace MessageService.Domain.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

public class Chat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ChatId { get; set; }

    [BsonElement("Participants")]
    public List<string> Participants { get; set; } = [];

    [BsonElement("LastMessageTimestamp")]
    public DateTime LastMessageTimestamp { get; set; }

    [BsonElement("ChatName")]
    public string ChatName { get; set; }
}
