using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizApp.ConsoleAdmin.Services
{
    public class QuestionService
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly QuizService _quizService;

        public QuestionService(IQuestionRepository questionRepo, QuizService quizService)
        {
            _questionRepo = questionRepo;
            _quizService = quizService;
        }

        public Question? SelectQuestions(string header)
        {
            var selectedQuiz = _quizService.SelectQuiz(header);
            if (selectedQuiz == null)
                return null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("(0 - aby wrócić do MENU)\n");
                string title = "==== Pytania z kategorii: ====";
                Console.WriteLine(title);
                Console.WriteLine($"{selectedQuiz.CategoryName}\n");

                var questions = _questionRepo.GetAll()
                    .Where(x => x.IdQuiz == selectedQuiz.IdQuiz)
                    .ToList();

                if (!questions.Any())
                {
                    Console.WriteLine("Brak pytań w tej kategorii.\n");

                    Thread.Sleep(2000);
                    return null;
                }

                foreach (var q in questions)
                    Console.WriteLine($"{q.IdQuestion}: {q.Contents}");

                Console.WriteLine("\n"+new string('=', header.Length-1));
                Console.Write("\nWprowadź numer pytania: ");

                string input = Console.ReadLine()?.Trim();

                if (input == "0")
                    return null;

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Niepoprawny format! Spróbuj ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                var question = questions.FirstOrDefault(q => q.IdQuestion == id);
                if (question == null)
                {
                    Console.WriteLine("Takie pytanie nie istnieje! Wybierz ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                return question; // wybrane pytanie
            }
        }
    }
}
