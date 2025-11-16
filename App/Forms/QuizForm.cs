using System;
using System.Linq;
using System.Windows.Forms;
using QuizApp.Models;

namespace QuizApp.Forms
{
    public partial class QuizForm : Form
    {
        private Quiz quiz;
        private int currentIndex = 0;
        private User user;
        private RadioButton[] optionButtons;

        public QuizForm(Quiz quiz)
        {
            this.quiz = quiz;
            this.user = new User { Name = "Player" };
            InitializeComponent();
            LoadQuestion();
        }

        private Label lblQuestion;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            this.Text = "Quiz";
            this.Width = 600;
            this.Height = 400;

            lblQuestion = new Label
            {
                Top = 30,
                Left = 30,
                Width = 520,
                Height = 60
            };
            this.Controls.Add(lblQuestion);

            optionButtons = new RadioButton[4];
            for (int i = 0; i < 4; i++)
            {
                optionButtons[i] = new RadioButton
                {
                    Top = 100 + i * 40,
                    Left = 50,
                    Width = 500
                };
                this.Controls.Add(optionButtons[i]);
            }

            btnSubmit = new Button
            {
                Text = "Submit",
                Width = 100,
                Height = 40,
                Top = 280,
                Left = 240
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);
        }

        private void LoadQuestion()
        {
            var q = quiz.Questions[currentIndex];
            lblQuestion.Text = q.Text;
            for (int i = 0; i < q.Options.Length; i++)
            {
                optionButtons[i].Text = q.Options[i];
                optionButtons[i].Checked = false;
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var selected = optionButtons.ToList().FindIndex(r => r.Checked);
            if (selected == -1)
            {
                MessageBox.Show("Wybierz odpowiedź!");
                return;
            }

            if (selected == quiz.Questions[currentIndex].CorrectOption)
                user.Score++;

            ResultsForm resultsForm = new ResultsForm(user.Score);
            resultsForm.ShowDialog();
            this.Close();
        }
    }
}
