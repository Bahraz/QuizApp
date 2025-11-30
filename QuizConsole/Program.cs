using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizApp.Data.Context;// Poprawna przestrzeń nazw dla QuizDbContext
using QuizApp.Data.Models;   // Poprawna przestrzeń nazw dla modeli

namespace QuizApp.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Wczytanie appsettings.json z katalogu startowego
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Utworzenie opcji DbContext
            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            // Utworzenie DbContext
            using var context = new QuizDbContext(options);

            // Testowe pobranie wszystkich quizów
            var quizzes = context.Quizzes.ToList();
            Console.WriteLine("Lista quizów w bazie:");
            foreach (var quiz in quizzes)
            {
                Console.WriteLine($"Quiz: {quiz.Title} (Id: {quiz.Id})");
            }

            Console.WriteLine("Gotowe. Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}
