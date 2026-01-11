using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizWeb.Pages.Quizzes
{
    public class QuizStartModel : PageModel
    {
        private readonly IQuizRepository _quizRepo;
        private readonly IAnswerRepository _answerRepo;

        public QuizStartModel(
            IQuizRepository quizRepo,
            IAnswerRepository answerRepo)
        {
            _quizRepo = quizRepo;
            _answerRepo = answerRepo;
        }

        public string QuizName { get; set; } = "";
        public List<Question> Questions { get; set; } = [];

        [BindProperty]
        public Dictionary<int, int> Answers { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var quiz = await _quizRepo.GetQuizWithQuestionsAndAnswersAsync(id);
            if (quiz == null) return NotFound();

            QuizName = quiz.CategoryName;

            Questions = quiz.Questions
                .OrderBy(q => Guid.NewGuid())
                .Take(5)
                .Select(q =>
                {
                    q.Answers = q.Answers
                        .OrderBy(a => Guid.NewGuid())
                        .ToList();
                    return q;
                })
                .ToList();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            int score = 0;

            foreach (var answerId in Answers.Values)
            {
                var answer = await _answerRepo.GetByIdAsync(answerId);
                if (answer != null && answer.IsTrue)
                    score++;
            }

            return RedirectToPage("Result", new
            {
                score,
                total = Answers.Count
            });
        }
    }
}
