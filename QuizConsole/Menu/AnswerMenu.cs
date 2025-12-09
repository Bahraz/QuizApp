using QuizApp.ConsoleAdmin.Services;
using QuizApp.Data.Entities;
using QuizApp.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.ConsoleAdmin.Menu;

public class AnswerMenu
{
    private readonly QuizService _quizService;
    private readonly QuestionService _questionService;
    private readonly AnswerService _answerService;
    private readonly IAnswerRepository _answerRepo;

    private const int WaitTime = 1500; // czas oczekiwania w ms

    public AnswerMenu(QuizService quizService, QuestionService questionService,
                      AnswerService answerService, IAnswerRepository answerRepo)
    {
        _quizService = quizService;
        _questionService = questionService;
        _answerService = answerService;
        _answerRepo = answerRepo;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            string header = "===== Zarządzanie odpowiedziami =====";
            Console.WriteLine(header + "\n");

            Console.WriteLine("1. Dodaj odpowiedź");
            Console.WriteLine("2. Usuń odpowiedź");
            Console.WriteLine("3. Edytuj odpowiedź");
            Console.WriteLine("4. Wyświetl listę odpowiedzi");
            Console.WriteLine("0. Powrót\n");
            Console.WriteLine(new string('=', header.Length));
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": await AddAnswer(); break;
                case "2": await DeleteAnswer(); break;
                case "3": await EditAnswer(); break;
                case "4": await ListAnswers(); break;
                case "0": return;
                default:
                    Console.WriteLine("\nNieznana opcja...");
                    Thread.Sleep(WaitTime); 
                    break;
            }
        }
    }

    private async Task AddAnswer()
    {
        string header = "** Dodaj odpowiedź do wybranego pytania **";
        var question = _questionService.SelectQuestions(header);
        if (question == null)
        {
            Console.WriteLine("Nie wybrano pytania.");
            Thread.Sleep(WaitTime);
            return;
        }

        var existingAnswers = _answerRepo.GetAll()
            .Where(a => a.IdQuestion == question.IdQuestion)
            .ToList();

        if (existingAnswers.Count >= 4)
        {
            Console.WriteLine("To pytanie ma już 4 odpowiedzi. Usuń jedną lub wybierz inne pytanie.");
            Thread.Sleep(WaitTime);
            return;
        }

        bool trueExists = existingAnswers.Any(a => a.IsTrue);
        int falseCount = existingAnswers.Count(a => !a.IsTrue);

    StartOver:
        Console.Clear();
        Console.WriteLine(header + "\n(0 - powrót)\n");
        Console.WriteLine($"Dodajesz odpowiedź do pytania:\n{question.Contents}\n");
        Console.WriteLine($"Aktualnie: {existingAnswers.Count} odpowiedzi (Prawdziwych: {(trueExists ? 1 : 0)} [MAX 1], Fałszywych: {falseCount} [MAX 3])\n");

        string text;
        bool? isTrue = null;

        // Treść odpowiedzi
        while (true)
        {
            Console.Write("Treść odpowiedzi: ");
            text = Console.ReadLine()?.Trim();
            if (text == "0") return;

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Nie wprowadzono treści odpowiedzi.");
                Thread.Sleep(WaitTime);
                goto StartOver;
            }

            if (existingAnswers.Any(a => string.Equals(a.Text?.Trim(), text, System.StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Taka odpowiedź już istnieje.");
                Thread.Sleep(WaitTime);
                goto StartOver;
            }

            break;
        }

        // Czy prawdziwa?
        while (true)
        {
            Console.Write("Czy odpowiedź jest prawdziwa? (t/n): ");
            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "0") return;
            if (input != "t" && input != "n")
            {
                Console.WriteLine("Podaj 't' lub 'n'.");
                continue;
            }

            bool userChoice = (input == "t");

            if (userChoice && trueExists)
            {
                Console.WriteLine("To pytanie ma już jedną prawdziwą odpowiedź. Dodaj fałszywą.");
                Thread.Sleep(WaitTime);
                goto StartOver;
            }

            if (!userChoice && falseCount >= 3)
            {
                Console.WriteLine("Maksymalnie 3 fałszywe odpowiedzi. Ta musi być prawdziwa.");
                Thread.Sleep(WaitTime);
                goto StartOver;
            }

            isTrue = userChoice;
            break;
        }

        var answer = new Answer
        {
            IdQuestion = question.IdQuestion,
            Text = text,
            IsTrue = isTrue.Value
        };

        await _answerRepo.AddAsync(answer);
        await _answerRepo.SaveChangesAsync();

        Console.WriteLine("Odpowiedź została dodana!");
        Thread.Sleep(WaitTime);
    }

    private async Task DeleteAnswer()
    {
        string header = "** Usuń odpowiedź **";

        // 1. Wybór pytania
        var question = _questionService.SelectQuestions(header);
        if (question == null)
        {
            Console.WriteLine("Nie wybrano pytania.");
            Thread.Sleep(WaitTime);
            return;
        }

        while (true)
        {
            // 2. Wybór odpowiedzi
            var answer = _answerService.SelectAnswer(question, header);
            if (answer == null)
            {
                Console.WriteLine("Powrót do menu.");
                Thread.Sleep(WaitTime);
                return;
            }

            // 3. Usunięcie odpowiedzi
            _answerRepo.Remove(answer);
            await _answerRepo.SaveChangesAsync();

            Console.WriteLine("Odpowiedź została usunięta!");
            Thread.Sleep(WaitTime);
            return; // zakończ po poprawnym usunięciu
        }
    }

    private async Task EditAnswer()
    {
        string header = "** Edytuj odpowiedź **";
        var question = _questionService.SelectQuestions(header);
        if (question == null)
        {
            Console.WriteLine("Nie wybrano pytania.");
            Thread.Sleep(WaitTime);
            return;
        }

        var answers = _answerRepo.GetAll().Where(a => a.IdQuestion == question.IdQuestion).ToList();
        if (!answers.Any())
        {
            Console.WriteLine("To pytanie nie ma odpowiedzi.");
            Thread.Sleep(WaitTime);
            return;
        }

        Console.Clear();
        Console.WriteLine(header + "\n(0 - powrót)\n");
        foreach (var a in answers)
            Console.WriteLine($"{a.IdAnswer}. {a.Text}  (Prawdziwa={a.IsTrue})");

        Console.WriteLine("\n" + new string('=', header.Length));
        Console.Write("Wybierz odpowiedź do edycji: ");

        if (!int.TryParse(Console.ReadLine(), out int id) || id == 0)
        {
            Console.WriteLine("Powrót do menu.");
            Thread.Sleep(WaitTime);
            return;
        }

        var ans = answers.FirstOrDefault(a => a.IdAnswer == id);
        if (ans == null)
        {
            Console.WriteLine("Nie znaleziono odpowiedzi.");
            Thread.Sleep(WaitTime);
            return;
        }

        bool trueExists = answers.Any(a => a.IsTrue && a.IdAnswer != ans.IdAnswer);
        int falseCount = answers.Count(a => !a.IsTrue && a.IdAnswer != ans.IdAnswer);



    StartEdit:
        Console.Clear();
        Console.WriteLine("** Edytujesz odpowiedź **\n(0 - powrót)\n");
        Console.WriteLine($"Aktualna treść: {ans.Text}");
        Console.WriteLine($"Prawdziwa: {ans.IsTrue}");
        Console.WriteLine(new string('=', 30)+"\n");

        Console.Write("Nowa treść: ");
        string newText = Console.ReadLine()?.Trim();
        if (newText == "0") return;
        if (!string.IsNullOrWhiteSpace(newText))
            ans.Text = newText;

        VerifyTruth:
        Console.Write($"Czy odpowiedź jest prawdziwa? (t/n): ");
        string inputTruth = Console.ReadLine()?.Trim().ToLower();
        if (inputTruth == "0") return;
        if (inputTruth != "t" && inputTruth != "n")
        {
            Console.WriteLine("Podaj 't' lub 'n'.");
            goto VerifyTruth;
        }

        bool userChoice = (inputTruth == "t");
        if (userChoice && trueExists)
        {
            Console.WriteLine("Inna odpowiedź jest już prawdziwa. Nie można dodać kolejnej.");
            Thread.Sleep(WaitTime);
            goto VerifyTruth;
        }
        if (!userChoice && falseCount >= 3)
        {
            Console.WriteLine("Maksymalnie 3 fałszywe odpowiedzi. Ta musi być prawdziwa.");
            Thread.Sleep(WaitTime);
            goto StartEdit;
        }

        ans.IsTrue = userChoice;
        await _answerRepo.SaveChangesAsync();

        Console.WriteLine("Zaktualizowano odpowiedź!");
        Thread.Sleep(WaitTime);
    }

    private async Task ListAnswers()
    {
        string header = "===== Lista odpowiedzi =====";
        var question = _questionService.SelectQuestions(header);
        if (question == null)
        {
            Console.WriteLine("Nie wybrano pytania.");
            Thread.Sleep(WaitTime);
            return;
        }

        var answers = _answerRepo.GetAll().Where(a => a.IdQuestion == question.IdQuestion).ToList();
        Console.Clear();
        Console.WriteLine(header + "\n");
        Console.WriteLine($"Pytanie: {question.Contents}\n");

        if (!answers.Any())
        {
            Console.WriteLine("Brak odpowiedzi dla tego pytania.");
        }
        else
        {
            foreach (var a in answers)
                Console.WriteLine($"{a.IdAnswer}: {a.Text}  [Prawdziwa={a.IsTrue}]");
        }

        Console.WriteLine("\n" + new string('=', header.Length));
        Console.WriteLine("Naciśnij dowolny klawisz...");
        Console.ReadKey();
    }
}
