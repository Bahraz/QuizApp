using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;
using QuizApp.ConsoleAdmin.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.ConsoleAdmin.Menu;

public class QuizMenu
{
    private readonly IQuizRepository _repo;
    private readonly IQuestionRepository _questionRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly QuizService _service;

    private const int WaitTime = 1500;

    public QuizMenu(IQuizRepository repo, IQuestionRepository questionRepo, IAnswerRepository answerRepo)
    {
        _repo = repo;
        _questionRepo = questionRepo;
        _answerRepo = answerRepo;
        _service = new QuizService(_questionRepo, _repo, _answerRepo);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            string header = "===== Zarządzanie kategoriami quizów =====";
            Console.WriteLine(header + "\n");

            Console.WriteLine("1. Dodaj kategorię");
            Console.WriteLine("2. Usuń kategorię");
            Console.WriteLine("3. Edytuj kategorię");
            Console.WriteLine("4. Wyświetl listę kategorii");
            Console.WriteLine("0. Powrót\n");
            Console.WriteLine(new string('=', header.Length));
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": await AddQuiz(); break;
                case "2": await DeleteQuiz(); break;
                case "3": await EditQuiz(); break;
                case "4": await ListQuizzesAsync(); break;
                case "0": return;
                default:
                    Console.WriteLine("\nNieznana opcja...");
                    Thread.Sleep(WaitTime);
                    break;
            }
        }
    }

    private async Task AddQuiz()
    {
        string header = "** Dodaj nową kategorię quizu **";

        while (true)
        {
            Console.Clear();
            Console.WriteLine(header + "\n");
            Console.WriteLine("(0 - aby wrócić do menu)\n");

            Console.Write("Podaj nazwę kategorii: ");
            string name = Console.ReadLine()?.Trim();

            if (name == "0") return;

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Nazwa kategorii nie może być pusta.");
                Thread.Sleep(WaitTime);
                continue;
            }

            var existing = await _repo.GetByNameAsync(name);
            if (existing != null)
            {
                Console.WriteLine($"Kategoria '{name}' już istnieje. Wybierz inną nazwę.");
                Thread.Sleep(WaitTime);
                continue;
            }

            await _repo.AddAsync(new Quiz { CategoryName = name });
            await _repo.SaveChangesAsync();

            Console.WriteLine("Kategoria została dodana!");
            Thread.Sleep(WaitTime);
            return;
        }
    }

    private async Task DeleteQuiz()
    {
        string header = "** Usuń kategorię quizu **";

        // 1. Wybór quizu przez QuizService
        var quiz = _service.SelectQuiz(header);
        if (quiz == null) // użytkownik wpisał 0 lub brak quizów
        {
            Console.WriteLine("Powrót do menu...");
            Thread.Sleep(WaitTime);
            return;
        }

        // 2. Pobranie quizu z pytaniami i odpowiedziami
        var quizToDelete = await _repo.GetQuizWithQuestionsAndAnswersAsync(quiz.IdQuiz);
        if (quizToDelete != null)
        {
            _repo.Remove(quizToDelete);
            await _repo.SaveChangesAsync();
            Console.WriteLine($"Usunięto kategorię '{quiz.CategoryName}' wraz z pytaniami i odpowiedziami!");
        }
        else
        {
            Console.WriteLine("Nie znaleziono quizu.");
        }

        Thread.Sleep(WaitTime);
        return;
    }

    private async Task EditQuiz()
    {
        string header = "** Edytuj kategorię quizu **";

        // 1. Wybór quizu
        var quiz = _service.SelectQuiz(header);
        if (quiz == null) // użytkownik wpisał 0 lub brak quizów
        {
            Console.WriteLine("Powrót do menu...");
            Thread.Sleep(WaitTime);
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine(header + "\n");
            Console.WriteLine($"Aktualna nazwa kategorii: {quiz.CategoryName}");
            Console.WriteLine("Wpisz nową nazwę (0 - powrót):");
            Console.Write("Nowa nazwa: ");

            string newName = Console.ReadLine()?.Trim();

            // Powrót do menu
            if (newName == "0")
                return;

            // Nic nie zmieniono
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Nie wprowadzono nazwy. Spróbuj ponownie.");
                Thread.Sleep(WaitTime);
                continue; // pozwala ponownie wprowadzić nazwę
            }

            // Treść taka sama jak aktualna
            if (newName.Equals(quiz.CategoryName, System.StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Wprowadzona nazwa jest taka sama jak aktualna. Spróbuj ponownie.");
                Thread.Sleep(WaitTime);
                continue; // ponowna próba
            }

            // Sprawdzenie duplikatu w innych quizach
            var duplicate = await _repo.GetByNameAsync(newName);
            if (duplicate != null && duplicate.IdQuiz != quiz.IdQuiz)
            {
                Console.WriteLine($"Kategoria '{newName}' już istnieje. Wprowadź inną nazwę.");
                Thread.Sleep(WaitTime);
                continue; // ponowna próba
            }

            // Zapis zmian
            quiz.CategoryName = newName;
            await _repo.SaveChangesAsync();

            Console.WriteLine("Zaktualizowano kategorię!");
            Thread.Sleep(WaitTime);
            return; // zakończ po poprawnym zapisaniu
        }
    }


    private async Task ListQuizzesAsync()
    {
        string header = "===== Lista kategorii quizów =====";
        Console.Clear();
        Console.WriteLine(header + "\n");

        var quizzes = _repo.GetAll().ToList();

        if (!quizzes.Any())
        {
            Console.WriteLine("Brak dostępnych kategorii quizów.\nNajpierw dodaj kategorię.");
        }
        else
        {
            foreach (var q in quizzes)
                Console.WriteLine($"{q.IdQuiz}: {q.CategoryName}");
        }

        Console.WriteLine("\n" + new string('=', header.Length));
        Console.WriteLine("Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }
}
