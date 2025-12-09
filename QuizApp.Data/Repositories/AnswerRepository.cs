using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizApp.Data.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(QuizDbContext db) : base(db) { }
    }
}
