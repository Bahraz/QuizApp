namespace QuizApp.Data.Entities
{
    public class Answer
    {
        public int IdAnswer { get; set; }
        public string Text { get; set; }
        public int IdQuestion { get; set; }
        public bool IsTrue { get; set; }
        public virtual Question Question { get; set; } = null!;
    }

}
