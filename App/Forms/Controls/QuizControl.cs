using QuizApp.Models;

public partial class QuizControl : UserControl
{
    public event EventHandler<int> AnswerSubmitted; // indeks wybranej odpowiedzi

    private Label lblQuestion;
    private RadioButton[] optionButtons;
    private Button btnSubmit;

    public QuizControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        lblQuestion = new Label { Top = 20, Left = 20, Width = 500 };
        Controls.Add(lblQuestion);

        optionButtons = new RadioButton[4];
        for (int i = 0; i < 4; i++)
        {
            optionButtons[i] = new RadioButton { Top = 70 + i * 30, Left = 40, Width = 400 };
            Controls.Add(optionButtons[i]);
        }

        btnSubmit = new Button { Text = "Zatwierdź", Top = 200, Left = 250, Width = 100 };
        btnSubmit.Click += BtnSubmit_Click;
        Controls.Add(btnSubmit);
    }

    public void LoadQuestion(Question q)
    {
        lblQuestion.Text = q.Text;
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

        AnswerSubmitted?.Invoke(this, selected);
    }
}
