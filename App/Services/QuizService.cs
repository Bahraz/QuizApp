using QuizApp.Models;

namespace QuizApp.Services
{
    public class QuizService
    {
        private readonly Quiz _quiz;

        public QuizService(Quiz quiz)
        {
            _quiz = quiz;
        }

        public int CalculateScore(User user)
        {
            // Placeholder – później logika liczenia punktów
            return user.Score;
        }
    }
}
