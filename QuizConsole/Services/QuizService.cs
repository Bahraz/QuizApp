using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;
using System.Linq;
using System.Threading;

namespace QuizApp.ConsoleAdmin.Services
{
    public class QuizService
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IQuizRepository _quizRepo;
        private readonly IAnswerRepository _answerRepo;

        public QuizService(IQuestionRepository questionRepo, IQuizRepository quizRepo, IAnswerRepository answerRepository)
        {
            _questionRepo = questionRepo;
            _quizRepo = quizRepo;
            _answerRepo = answerRepository;
        }

        public Quiz? SelectQuiz(string header)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("(0 - aby wrócić do MENU)\n");
                string title = "==== Wybierz kategorię ====";
                Console.WriteLine(title);

                var quizzes = _quizRepo.GetAll().ToList();
                if (!quizzes.Any())
                {
                    Console.WriteLine("Brak dostępnych kategorii. Najpierw dodaj kategorię.");
                    Thread.Sleep(2000);
                    return null;
                }

                foreach (var q in quizzes)
                    Console.WriteLine($"{q.IdQuiz}. {q.CategoryName}");

                Console.WriteLine("\n" + new string('=', header.Length - 1));

                Console.Write("\nWprowadź numer kategorii: ");
                string input = Console.ReadLine()?.Trim();

                if (input == "0")
                    return null;

                if (!int.TryParse(input, out int quizId))
                {
                    Console.WriteLine("Niepoprawny format! Spróbuj ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                var selected = quizzes.FirstOrDefault(q => q.IdQuiz == quizId);
                if (selected == null)
                {
                    Console.WriteLine("Taka kategoria nie istnieje! Wybierz ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                return selected;
            }
        }
    }
}
