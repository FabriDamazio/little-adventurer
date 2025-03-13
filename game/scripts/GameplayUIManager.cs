using Godot;

public partial class GameplayUIManager : Node3D
{
    [Export]
    public PlayerCharacter Player;

    [Export]
    public ProgressBar HealthBar;

    [Export]
    public ColorRect BlackColorRect;

    [Export]
    public CenterContainer GameFinish;

    [Export]
    public CenterContainer GameOver;

    [Export]
    public CenterContainer GamePause;

    public Label LabelCoin;


    public override void _Ready()
    {
        LabelCoin = GetNode<Label>("%LabelCoin");
        Player.CoinNumberUpdated += UpdateCoinLabel;

        UpdateHealthBar(Player.CurrentHealth, Player.MaxHealth);
        Player.PlayerHealthUpdated += UpdateHealthBar;

        TogglePauseUI(false);
        ToggleGameOverUI(false);
        ToggleGameFinishUI(false);
    }

    public void UpdateCoinLabel(int value)
    {
        LabelCoin.Text = value.ToString();
    }

    public void UpdateHealthBar(int newValue, int maxValue)
    {
        var percentage = (float)newValue / (float)maxValue * 100;
        HealthBar.Value = percentage;
    }

    public void TogglePauseUI(bool toogle)
    {
        BlackColorRect.Visible = toogle;
        GamePause.Visible = toogle;
    }

    public void ToggleGameOverUI(bool toogle)
    {
        BlackColorRect.Visible = toogle;
        GameOver.Visible = toogle;
    }

    public void ToggleGameFinishUI(bool toogle)
    {
        BlackColorRect.Visible = toogle;
        GameFinish.Visible = toogle;
    }
}
