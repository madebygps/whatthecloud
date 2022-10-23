using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class Author
    {
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } 
        [BsonElement("link")]
        [JsonPropertyName("url")]
        public string Url { get; set; } 
    }
}
