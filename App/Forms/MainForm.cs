using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuizApp.Data;
using QuizApp.Models;
using System.Threading.Tasks;

namespace QuizApp.Forms
{
    public partial class MainForm : Form
    {
        private QuizRepository _quizRepository;
        private List<Question> _questions = new List<Question>();
        private int _currentIndex = 0;
        private int _score = 0;

        private Panel panelMenu;
        private Panel panelQuiz;
        private Panel panelResults;

        private Label lblQuestion;
        private RadioButton[] optionButtons;
        private Button btnSubmit;
        private Button btnStart;

        public MainForm()
        {
            InitializeComponent();

            // inicjalizacja repozytorium MongoDB
            string connStr = "mongodb+srv://quizappadmin:1X2k6Y1wvxYl9tW1@absolutecinema.iqbqe2g.mongodb.net/quizapp"; // Twój connection string
            string dbName = "quizapp";                     // nazwa bazy danych
            string collectionName = "questions";          // nazwa kolekcji
            _quizRepository = new QuizRepository(connStr, dbName, collectionName);

            ShowMenu();
        }

        private void InitializeComponent()
        {
            this.Text = "Quiz App";
            this.Width = 600;
            this.Height = 400;

            // PANEL MENU
            panelMenu = new Panel { Dock = DockStyle.Fill };
            btnStart = new Button
            {
                Text = "Rozpocznij Quiz",
                Width = 120,
                Height = 40,
                Top = 120,
                Left = 240
            };
            btnStart.Click += BtnStart_Click; // async void handler zamiast lambda
            panelMenu.Controls.Add(btnStart);
            this.Controls.Add(panelMenu);

            // PANEL QUIZ
            panelQuiz = new Panel { Dock = DockStyle.Fill, Visible = false };
            lblQuestion = new Label { Top = 20, Left = 20, Width = 500 };
            panelQuiz.Controls.Add(lblQuestion);

            optionButtons = new RadioButton[4];
            for (int i = 0; i < 4; i++)
            {
                optionButtons[i] = new RadioButton { Top = 70 + i * 30, Left = 40, Width = 400 };
                panelQuiz.Controls.Add(optionButtons[i]);
            }

            btnSubmit = new Button { Text = "Zatwierdź", Top = 200, Left = 250, Width = 100 };
            btnSubmit.Click += BtnSubmit_Click;
            panelQuiz.Controls.Add(btnSubmit);

            this.Controls.Add(panelQuiz);

            // PANEL WYNIKÓW
            panelResults = new Panel { Dock = DockStyle.Fill, Visible = false };
            Label lblResult = new Label { Name = "lblResult", Top = 100, Left = 220, Width = 200 };
            panelResults.Controls.Add(lblResult);

            Button btnBackToMenu = new Button { Text = "Powrót do menu", Top = 200, Left = 230, Width = 130 };
            btnBackToMenu.Click += (s, e) => ShowMenu();
            panelResults.Controls.Add(btnBackToMenu);

            this.Controls.Add(panelResults);
        }

        private void ShowMenu()
        {
            panelMenu.Visible = true;
            panelQuiz.Visible = false;
            panelResults.Visible = false;
        }

        // async void handler zamiast lambdy
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            await StartQuizAsync();
        }

        private async Task StartQuizAsync()
        {
            try
            {
                _questions = await _quizRepository.GetAllQuestionsAsync();
                _score = 0;
                _currentIndex = 0;

                if (_questions.Count == 0)
                {
                    MessageBox.Show("Brak pytań w bazie!");
                    return;
                }

                ShowQuiz();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania pytań: " + ex.Message);
            }
        }

        private void ShowQuiz()
        {
            panelMenu.Visible = false;
            panelQuiz.Visible = true;
            panelResults.Visible = false;

            LoadCurrentQuestion();
        }

        private void ShowResults()
        {
            panelMenu.Visible = false;
            panelQuiz.Visible = false;
            panelResults.Visible = true;

            var lblResult = panelResults.Controls["lblResult"] as Label;
            lblResult.Text = $"Twój wynik: {_score}/{_questions.Count}";
        }

        private void LoadCurrentQuestion()
        {
            if (_currentIndex >= _questions.Count)
            {
                ShowResults();
                return;
            }

            var q = _questions[_currentIndex];
            lblQuestion.Text = q.Text;

            // dynamiczne ustawienie odpowiedzi
            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < q.Answers.Length)
                {
                    optionButtons[i].Text = q.Answers[i];
                    optionButtons[i].Visible = true;
                    optionButtons[i].Checked = false;
                }
                else
                {
                    optionButtons[i].Visible = false;
                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int selected = -1;
            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (optionButtons[i].Checked)
                    selected = i;
            }

            if (selected == -1)
            {
                MessageBox.Show("Wybierz odpowiedź!");
                return;
            }

            var currentQuestion = _questions[_currentIndex];
            if (selected == currentQuestion.CorrectIndex)
                _score++;

            _currentIndex++;
            LoadCurrentQuestion();
        }
    }
}
