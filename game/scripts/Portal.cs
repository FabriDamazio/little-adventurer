using Godot;

public partial class Portal : Node3D
{
    public int CoinRequired = 3;

    [Export]
    public Label TextLabel;

    [Export]
    public Area3D Area3D;

    public override void _Ready()
    {
        TextLabel.Text = CoinRequired.ToString();
        Area3D.BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node3D body)
    {
        if (body.IsInGroup("player"))
        {
          var player = body as PlayerCharacter;

          if(player.CoinNumber >= CoinRequired)
          {
            GD.Print("GREAT");
          }
          else
          {
            GD.Print("NOT GREAT");
          }
        }
    }
}
