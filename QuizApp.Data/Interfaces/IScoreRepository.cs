using QuizApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IScoreRepository : IRepository<Score>
    {
        Task<IEnumerable<Score>> GetScoresByQuizAsync(int quizId);
    }
}
