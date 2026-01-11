using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizWeb.Pages.Quizzes
{
    public class QuizIndexModel : PageModel
    {
        private readonly IQuizRepository _quizRepo;

        public QuizIndexModel(IQuizRepository quizRepo)
        {
            _quizRepo = quizRepo;
        }

        public IEnumerable<Quiz> Quizzes { get; set; } = [];

        public void OnGet()
        {
            Quizzes = _quizRepo.GetAll();
        }
    }
}
