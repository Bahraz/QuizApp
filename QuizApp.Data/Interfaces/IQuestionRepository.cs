using QuizApp.Data.Entities;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetAllAsync();
        Task<Question?> GetByIdAsync(int id);
        Task AddAsync(Question question);
        void Remove(Question question);
        Task SaveChangesAsync();
        Task<bool> QuestionExistsAsync(int quizId, string text);
        Task<Question?> GetQuestionWithAnswersAsync(int questionId);
    }

}
