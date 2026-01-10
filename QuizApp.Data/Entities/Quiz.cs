using System.Collections.Generic;

namespace QuizApp.Data.Entities
{
    public class Quiz
    {
        public int IdQuiz { get; set; }

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}