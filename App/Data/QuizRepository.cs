using MongoDB.Bson;
using MongoDB.Driver;
using QuizApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Data
{
    public class QuizRepository
    {
        private readonly IMongoCollection<Question> _questionsCollection;

        public QuizRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _questionsCollection = database.GetCollection<Question>(collectionName);
        }

        // Pobranie wszystkich pytań
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _questionsCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Pobranie pytań po kategorii
        public async Task<List<Question>> GetQuestionsByCategoryAsync(string category)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.Category, category);
            return await _questionsCollection.Find(filter).ToListAsync();
        }

        // Pobranie pytań po poziomie trudności
        public async Task<List<Question>> GetQuestionsByDifficultyAsync(string difficulty)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.Difficulty, difficulty);
            return await _questionsCollection.Find(filter).ToListAsync();
        }

        // Pobranie jednego pytania po Id
        public async Task<Question?> GetQuestionByIdAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.Id, new ObjectId(id));
            return await _questionsCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
