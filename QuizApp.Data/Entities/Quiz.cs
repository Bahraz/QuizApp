using System.Collections.Generic;

namespace QuizApp.Data.Entities
{
    public class Quiz
    {
        // Klucz główny
        public int IdQuiz { get; set; }

        public string CategoryName { get; set; } = null!;

        // Nawigacja do pytań
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}