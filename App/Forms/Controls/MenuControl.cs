public partial class MenuControl : UserControl
{
    public event EventHandler StartClicked;

    private Button btnStart;

    public MenuControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        btnStart = new Button
        {
            Text = "Rozpocznij Quiz",
            Width = 120,
            Height = 40,
            Top = 120,
            Left = 240
        };
        btnStart.Click += (s, e) => StartClicked?.Invoke(this, EventArgs.Empty);
        Controls.Add(btnStart);
    }
}
