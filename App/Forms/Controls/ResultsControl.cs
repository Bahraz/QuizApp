public partial class ResultsControl : UserControl
{
    private Label lblResult;
    private Button btnBack;

    public event EventHandler BackClicked;

    public ResultsControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        lblResult = new Label { Top = 100, Left = 220, Width = 200 };
        Controls.Add(lblResult);

        btnBack = new Button { Text = "Powrót do menu", Top = 200, Left = 230, Width = 130 };
        btnBack.Click += (s, e) => BackClicked?.Invoke(this, EventArgs.Empty);
        Controls.Add(btnBack);
    }

    public void SetScore(int score, int total)
    {
        lblResult.Text = $"Twój wynik: {score}/{total}";
    }
}
