using QuizApp.ConsoleAdmin.Services;
using QuizApp.Data.Interfaces;

namespace QuizApp.ConsoleAdmin.Menu;

public class MainMenu
{
    private readonly IQuizRepository _quizRepo;
    private readonly IQuestionRepository _questionRepo;
    private readonly IAnswerRepository _answerRepo;

    private const int WaitTime = 1500;

    public MainMenu(IQuizRepository quizRepo,
                    IQuestionRepository questionRepo,
                    IAnswerRepository answerRepo)
    {
        _quizRepo = quizRepo;
        _questionRepo = questionRepo;
        _answerRepo = answerRepo;
    }

    public async Task RunAsync()
    {
        var quizService = new QuizService(_questionRepo, _quizRepo, _answerRepo);
        var questionService = new QuestionService(_questionRepo, quizService);
        var answerService = new AnswerService(_answerRepo);

        while (true)
        {
            Console.Clear();
            string header = "===== Quiz Admin Panel =====";
            Console.WriteLine(header + "\n");

            Console.WriteLine("1. Zarządzaj quizami");
            Console.WriteLine("2. Zarządzaj pytaniami");
            Console.WriteLine("3. Zarządzaj odpowiedziami");
            Console.WriteLine("0. Wyjdź\n");
            Console.WriteLine(new string('=', header.Length));
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    await new QuizMenu(_quizRepo, _questionRepo, _answerRepo).RunAsync();
                    break;
                case "2":
                    await new QuestionMenu(_questionRepo, _quizRepo, _answerRepo).RunAsync();
                    break;
                case "3":
                    await new AnswerMenu(quizService, questionService, answerService, _answerRepo).RunAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nNieznana opcja...");
                    Thread.Sleep(WaitTime);
                    break;
            }
        }
    }
}
