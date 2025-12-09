using QuizApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IQuizRepository
    {
        Task AddAsync(Quiz quiz);
        void Remove(Quiz quiz);
        Task SaveChangesAsync();

        Task<Quiz?> GetByIdAsync(int idQuiz);
        Task<Quiz?> GetByNameAsync(string name);
        Task<Quiz?> GetQuizWithQuestionsAsync(int idQuiz);
        Task<Quiz?> GetQuizWithQuestionsAndAnswersAsync(int idQuiz);
        IEnumerable<Quiz> GetAll();
    }
}
