using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;
using QuizApp.ConsoleAdmin.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.ConsoleAdmin.Menu;

public class QuestionMenu
{
    private readonly IQuestionRepository _repo;
    private readonly IQuizRepository _quizRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly QuizService _quizService;
    private readonly QuestionService _questionService;

    private const int WaitTime = 1500;

    public QuestionMenu(IQuestionRepository questionRepo, IQuizRepository quizRepo, IAnswerRepository answerRepo)
    {
        _repo = questionRepo;
        _quizRepo = quizRepo;
        _answerRepo = answerRepo;

        _quizService = new QuizService(_repo, _quizRepo, _answerRepo);
        _questionService = new QuestionService(_repo, _quizService);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            string header = "===== Zarządzanie pytaniami =====";
            Console.WriteLine(header + "\n");

            Console.WriteLine("1. Dodaj pytanie");
            Console.WriteLine("2. Usuń pytanie");
            Console.WriteLine("3. Edytuj pytanie");
            Console.WriteLine("4. Wyświetl listę pytań");
            Console.WriteLine("0. Powrót\n");
            Console.WriteLine(new string('=', header.Length));
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": await AddQuestion(); break;
                case "2": await DeleteQuestion(); break;
                case "3": await EditQuestion(); break;
                case "4": await ListQuestions(); break;
                case "0": return;
                default:
                    Console.WriteLine("Nieznana opcja...");
                    Thread.Sleep(WaitTime);
                    break;
            }
        }
    }

    private async Task AddQuestion()
    {
        string header = "** Dodaj pytanie **";
        var quiz = _quizService.SelectQuiz(header);
        if (quiz == null)
        {
            Console.WriteLine("Nie wybrano kategorii.");
            Thread.Sleep(WaitTime);
            return;
        }

        Console.Clear();
        Console.WriteLine(header + "\n");
        Console.WriteLine($"Wybrana kategoria: {quiz.CategoryName}\n");
        Console.WriteLine("(0 - powrót)\n");

        while (true)
        {
            Console.Write("Treść pytania: ");
            string text = Console.ReadLine()?.Trim();
            if (text == "0") return;

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Treść pytania nie może być pusta.");
                Thread.Sleep(WaitTime);
                continue;
            }

            if (await _repo.QuestionExistsAsync(quiz.IdQuiz, text))
            {
                Console.WriteLine("Takie pytanie już istnieje.");
                Thread.Sleep(WaitTime);
                continue;
            }

            await _repo.AddAsync(new Question { IdQuiz = quiz.IdQuiz, Contents = text });
            await _repo.SaveChangesAsync();
            Console.WriteLine("Pytanie dodane!");
            Thread.Sleep(WaitTime);
            return;
        }
    }

    private async Task DeleteQuestion()
    {
        string header = "** Usuń pytanie **";

        // 1. Wybór pytania
        var question = _questionService.SelectQuestions(header);
        if (question == null) // użytkownik wpisał 0 lub brak pytań
        {
            Console.WriteLine("Powrót do menu...");
            Thread.Sleep(WaitTime);
            return;
        }

        // 2. Pobranie pytania wraz z odpowiedziami
        var questionWithAnswers = await _repo.GetQuestionWithAnswersAsync(question.IdQuestion);

        if (questionWithAnswers != null)
        {
            // 3. Usuń odpowiedzi jeśli są
            if (questionWithAnswers.Answers != null && questionWithAnswers.Answers.Any())
                _answerRepo.RemoveRange(questionWithAnswers.Answers);

            // 4. Usuń pytanie
            _repo.Remove(questionWithAnswers);
            await _repo.SaveChangesAsync();

            Console.WriteLine("Pytanie i powiązane odpowiedzi usunięte.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono pytania.");
        }

        Thread.Sleep(WaitTime);
        return;
    }

    private async Task EditQuestion()
    {
        string header = "** Edytuj pytanie **";

        // 1. Wybór pytania
        var question = _questionService.SelectQuestions(header);
        if (question == null)
        {
            Console.WriteLine("Nie wybrano pytania.");
            Thread.Sleep(WaitTime);
            return;
        }

        // 2. Wyświetlenie aktualnej treści
    StartEdit:

        Console.Clear();
        Console.WriteLine(header + "\n");
        Console.WriteLine($"Aktualna treść: {question.Contents}");
        Console.WriteLine(new string('=', 30)+"\n");

        Console.Write("Nowa treść pytania: ");
        string newText = Console.ReadLine()?.Trim();

        if (newText == "0")
            return;

        if (string.IsNullOrWhiteSpace(newText))
        {
            Console.WriteLine("Treść nie zmieniona. Wprowadź pytanie ponownie.");
            Thread.Sleep(WaitTime);
            goto StartEdit;
        }

        // 3. Sprawdzenie czy treść nie jest taka sama jak aktualna
        if (newText.Equals(question.Contents, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Treść jest taka sama jak poprzednia. Wprowadź pytanie ponownie.");
            Thread.Sleep(WaitTime);
            goto StartEdit;
        }

        // 4. Sprawdzenie czy w kategorii nie istnieje już takie pytanie
        bool duplicateExists = await _repo.QuestionExistsAsync(question.IdQuiz, newText);
        if (duplicateExists)
        {
            Console.WriteLine("W tej kategorii istnieje już takie pytanie. Wprowadź inną treść.");
            Thread.Sleep(WaitTime);
            goto StartEdit;
        }

        // 5. Zapis zmian
        question.Contents = newText;
        await _repo.SaveChangesAsync();

        Console.WriteLine("Pytanie zostało zaktualizowane!");
        Thread.Sleep(WaitTime);
    }

    private async Task ListQuestions()
    {
        string header = "===== Lista pytań =====";
        var quiz = _quizService.SelectQuiz(header);
        if (quiz == null)
        {
            Console.WriteLine("Brak wybranej kategorii.");
            Thread.Sleep(WaitTime);
            return;
        }

        Console.Clear();
        Console.WriteLine(header + "\n");
        Console.WriteLine($"[Pytania z kategorii: {quiz.CategoryName}]\n");

        var questions = _repo.GetAll().Where(q => q.IdQuiz == quiz.IdQuiz).ToList();
        if (!questions.Any())
        {
            Console.WriteLine("Brak pytań w tej kategorii.");
            Thread.Sleep(WaitTime);
            return;
        }

        foreach (var q in questions)
            Console.WriteLine($"{q.IdQuestion}: {q.Contents}");

        Console.WriteLine("\n" + new string('=', header.Length));
        Console.WriteLine("Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }
}
