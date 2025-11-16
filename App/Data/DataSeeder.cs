using QuizApp.Models;
using System.Collections.Generic;

namespace QuizApp.Data
{
    public static class DataSeeder
    {
        public static Quiz SeedQuiz()
        {
            var quiz = new Quiz();
            quiz.Questions.Add(new Question
            {
                Text = "Jaki jest największy ocean na Ziemi?",
                Options = new string[] { "Atlantycki", "Spokojny", "Indyjski", "Arktyczny" },
                CorrectOption = 1 // Spokojny
            });
            return quiz;
        }
    }
}
