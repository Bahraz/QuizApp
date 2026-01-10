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
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<QuizDbContext>().UseSqlServer(connectionString).Options;

        using var db = new QuizDbContext(options);

        var quizRepo = new QuizRepository(db);
        var questionRepo = new QuestionRepository(db);
        var answerRepo = new AnswerRepository(db);

        var mainMenu = new MainMenu(quizRepo, questionRepo, answerRepo);
        await mainMenu.RunAsync();
    }
}
