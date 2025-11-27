using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuizApp.Models
{
    public class Question
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("question")]
        public required string Text { get; set; }

        [BsonElement("answers")]
        public required string[] Answers { get; set; }

        [BsonElement("correctIndex")]
        public int CorrectIndex { get; set; }

        [BsonElement("category")]
        public required string Category { get; set; }

        [BsonElement("difficulty")]
        public required string Difficulty { get; set; }
    }
}
