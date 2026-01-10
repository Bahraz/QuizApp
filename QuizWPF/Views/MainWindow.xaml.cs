using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizApp.Data.Repositories;
using QuizWPF.ViewModels;
using System.IO;
using System.Windows;

namespace QuizWPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<QuizDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var db = new QuizDbContext(options);

            var quizRepo = new QuizRepository(db);
            DataContext = new MainViewModel(quizRepo);
        }
    }
}
