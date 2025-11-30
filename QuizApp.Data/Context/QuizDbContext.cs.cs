using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Models;

namespace QuizApp.Data.Context
{
    public class QuizDbContext : DbContext
    {
        // Konstruktor używany w runtime (wstrzykiwanie DbContextOptions)
        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }

        // Tabele w bazie
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacje:

            // Quiz -> Questions (1:N)
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Question -> Answers (1:N)
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Score -> Quiz (N:1)
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Quiz)
                .WithMany()
                .HasForeignKey(s => s.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ograniczenia kolumn
            modelBuilder.Entity<Question>()
                .Property(q => q.Text)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Answer>()
                .Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Quiz>()
                .Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
