using System;
using System.Windows.Forms;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Forms
{
    public partial class MainForm : Form
    {
        private Quiz quiz;

        public MainForm()
        {
            InitializeComponent();
            quiz = DataSeeder.SeedQuiz();
        }

        private void InitializeComponent()
        {
            this.Text = "Quiz App - Main Menu";
            this.Width = 400;
            this.Height = 300;

            Button startButton = new Button
            {
                Text = "Start Quiz",
                Width = 120,
                Height = 40,
                Top = 100,
                Left = (this.ClientSize.Width - 120) / 2
            };
            startButton.Click += StartButton_Click;

            this.Controls.Add(startButton);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            QuizForm quizForm = new QuizForm(quiz);
            quizForm.ShowDialog();
        }
    }
}
