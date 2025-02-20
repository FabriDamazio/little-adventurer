using Godot;

public partial class GameplayUIManager : Control
{
    [Export]
    public PlayerCharacter Player;

    public Label LabelCoin;

    public override void _Ready()
    {
        LabelCoin = GetNode<Label>("%LabelCoin");
        Player.CoinNumberUpdated += UpdateCoinLabel;
    }

    public void UpdateCoinLabel(int value)
    {
        LabelCoin.Text = value.ToString();
    }

}
