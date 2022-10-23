using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class Definition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("word")]
        [JsonPropertyName("word")]
        public string Word { get; set; } 
       
        [BsonElement("definition")]
        [JsonPropertyName("definition")]
        public string Content { get; set; } 

        [BsonElement("author")]
        [JsonPropertyName("author")]
        public Author Author { get; set; } 

        [BsonElement("learnMoreUrl")]
        [JsonPropertyName("learnMoreUrl")]
        public string LearnMoreUrl { get; set; } 

        [BsonElement("tag")]
        [JsonPropertyName("tag")]
        public string Tag { get; set; } 

        [BsonElement("abbreviation")]
        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

    }
}
