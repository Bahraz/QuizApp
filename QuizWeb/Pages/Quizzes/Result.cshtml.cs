using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizWeb.Pages.Quizzes
{
    public class ResultModel : PageModel
    {
        public string UserName { get; set; } = "";
        public int Score { get; set; }
        public int Total { get; set; }

        public void OnGet(int score, int total)
        {
            UserName = HttpContext.Session.GetString("UserName") ?? "Gracz";
            Score = score;
            Total = total;
        }
    }
}
