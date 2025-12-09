using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QuizApp.Data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizDbContext _context;

        public QuizRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
        }

        public void Remove(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int idQuiz)
        {
            return await _context.Quizzes.FindAsync(idQuiz);
        }

        public async Task<Quiz?> GetByNameAsync(string name)
        {
            return await _context.Quizzes
                .FirstOrDefaultAsync(q => q.CategoryName.ToLower() == name.ToLower());
        }

        public async Task<Quiz?> GetQuizWithQuestionsAsync(int idQuiz)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);
        }

        public async Task<Quiz?> GetQuizWithQuestionsAndAnswersAsync(int idQuiz)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);
        }

        public IEnumerable<Quiz> GetAll()
        {
            return _context.Quizzes.ToList();
        }
    }
}
