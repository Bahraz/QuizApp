using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Entities;

public class QuizDbContext : DbContext
{
    public DbSet<Quiz> Quizzes { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;

    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quiz>()
            .HasKey(q => q.IdQuiz);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.IdQuestion);

        modelBuilder.Entity<Answer>()
            .HasKey(a => a.IdAnswer);

        modelBuilder.Entity<Quiz>()
            .HasMany(q => q.Questions)
            .WithOne(q => q.Quiz)
            .HasForeignKey(q => q.IdQuiz)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.IdQuestion)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Quiz>()
            .Property(q => q.CategoryName)
            .IsRequired();
    }
}
