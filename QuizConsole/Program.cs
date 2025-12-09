using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizApp.ConsoleAdmin.Menu;
using QuizApp.Data;
using QuizApp.Data.Repositories;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        //
        // 🔥 1. Wczytanie appsettings.json
        //
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // katalog aplikacji konsolowej
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        //
        // 🔥 2. Pobranie connection string
        //
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        //
        // 🔥 3. Tworzymy DbContext tak samo jak Fabryka
        //
        var options = new DbContextOptionsBuilder<QuizDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        using var db = new QuizDbContext(options);

        //
        // 🔥 4. Tworzymy repozytoria
        //
        var quizRepo = new QuizRepository(db);
        var questionRepo = new QuestionRepository(db);
        var answerRepo = new AnswerRepository(db);

        //
        // 🔥 5. Uruchamiamy menu
        //
        var mainMenu = new MainMenu(quizRepo, questionRepo, answerRepo);
        await mainMenu.RunAsync();
    }
}
