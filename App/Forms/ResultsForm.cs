using System.Windows.Forms;

namespace QuizApp.Forms
{
    public partial class ResultsForm : Form
    {
        private int score;

        public ResultsForm(int score)
        {
            this.score = score;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Results";
            this.Width = 400;
            this.Height = 300;

            Label lblScore = new Label
            {
                Text = $"Twój wynik: {score}",
                Top = 100,
                Left = 120,
                Width = 200,
                Height = 50
            };
            this.Controls.Add(lblScore);

            Button btnClose = new Button
            {
                Text = "Zamknij",
                Top = 180,
                Left = 150,
                Width = 100
            };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }
    }
}
