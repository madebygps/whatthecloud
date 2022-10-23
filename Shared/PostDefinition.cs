using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class PostDefinition
    {
     

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

