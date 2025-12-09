using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;

namespace QuizApp.ConsoleAdmin.Services
{
    public class AnswerService
    {
        private readonly IQuestionRepository _questionRepo;
        private readonly IQuizRepository _quizRepo;
        private readonly IAnswerRepository _answerRepo;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepo = answerRepository;
        }

        public Answer? SelectAnswer(Question question, string header)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("(0 - aby wrócić do MENU)\n");
                string title = "==== Odpowiedzi do pytania: ====";
                Console.WriteLine(title);
                Console.WriteLine($"{question.Contents}\n");

                var answers = _answerRepo.GetAll()
                    .Where(a => a.IdQuestion == question.IdQuestion)
                    .ToList();

                if (!answers.Any())
                {
                    Console.WriteLine("To pytanie nie ma żadnych odpowiedzi.");
                    Thread.Sleep(2000);
                    return null;
                }

                foreach (var a in answers)
                    Console.WriteLine($"{a.IdAnswer}. {a.Text}  (Prawdziwa={(a.IsTrue ? "TAK" : "NIE")})");

                Console.WriteLine("\n" + new string('=', header.Length - 1));
                Console.Write("Wybierz odpowiedź: ");

                string input = Console.ReadLine()?.Trim();

                if (input == "0")
                    return null;

                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Niepoprawny format! Spróbuj ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                var answer = answers.FirstOrDefault(a => a.IdAnswer == id);
                if (answer == null)
                {
                    Console.WriteLine("Taka odpowiedź nie istnieje! Wybierz ponownie.");
                    Thread.Sleep(1500);
                    continue;
                }

                return answer;
            }
        }
    }
}
