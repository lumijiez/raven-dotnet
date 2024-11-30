using MongoDB.Bson.Serialization.Attributes;

namespace Raven.Message.Domain.Entities;

public class UserProfile
{
    [BsonElement("UserId")]
    public string UserId { get; set; } 
    
    [BsonElement("ProfilePictureUrl")]
    public string ProfilePictureUrl { get; set; }
    
    [BsonElement("DisplayName")]
    public string DisplayName { get; set; } 
    
    [BsonElement("Username")]
    public string Username { get; set; } 
    
    [BsonElement("Bio")]
    public string Bio { get; set; } 
    
    [BsonElement("JoinedDate")]
    public DateTime JoinedDate { get; set; } 
}