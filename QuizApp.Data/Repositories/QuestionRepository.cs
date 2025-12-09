using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizApp.Data.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private readonly QuizDbContext _context;

        public QuestionRepository(QuizDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public void Remove(Question question)
        {
            _context.Questions.Remove(question);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> QuestionExistsAsync(int quizId, string text)
        {
            return await _context.Questions
                                 .AnyAsync(q => q.IdQuiz == quizId &&
                                                q.Contents.Trim().ToLower() == text.Trim().ToLower());
        }
        public async Task<Question?> GetQuestionWithAnswersAsync(int questionId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.IdQuestion == questionId);
        }
    }
}
