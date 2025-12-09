namespace QuizApp.Data.Entities
{
    public class Question
    {
        public int IdQuestion { get; set; }
        public string Contents { get; set; }
        public int IdQuiz { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

}
