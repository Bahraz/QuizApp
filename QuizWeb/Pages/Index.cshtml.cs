using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizWeb.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            Console.WriteLine("=== INDEX ONPOST HIT ===");
            Console.WriteLine($"USERNAME FROM FORM = '{UserName}'");

            HttpContext.Session.SetString("UserName", UserName);
            return RedirectToPage("/Quizzes/Index");
        }
    }
}
