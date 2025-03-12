using Godot;

public partial class GameplayUIManager : Control
{
    [Export]
    public PlayerCharacter Player;

    [Export]
    public ProgressBar HealthBar;

    public Label LabelCoin;

    public override void _Ready()
    {
        LabelCoin = GetNode<Label>("%LabelCoin");
        Player.CoinNumberUpdated += UpdateCoinLabel;

        UpdateHealthBar(Player.CurrentHealth, Player.MaxHealth);
        Player.PlayerHealthUpdated += UpdateHealthBar;
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

}
