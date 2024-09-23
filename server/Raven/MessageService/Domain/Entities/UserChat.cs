namespace MessageService.Domain.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class UserChat
{
    [BsonId]
    public string UserId { get; set; } 

    [BsonElement("ChatIds")]
    public List<string> ChatIds { get; set; } = [];
}

