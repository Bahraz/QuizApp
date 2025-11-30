using QuizApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<Quiz> GetQuizWithQuestionsAsync(int id);
    }
}
