using Godot;

public partial class CollectableHealingOrb : Node3D
{
    [Export]
    public Node3D Visual;

    [Export]
    public Area3D Area3D;

    [Export]
    public AnimationPlayer VFXAnimationPlayer;

    public int HealthValue = 30;

    public override void _Ready()
    {
        Area3D.BodyEntered += OnArea3DBodyEntered;
    }

    public override void _Process(double delta)
    {
        if (Visual.Visible is false && VFXAnimationPlayer.IsPlaying() is false)
        {
            QueueFree();
        }
    }

    public void OnArea3DBodyEntered(Node3D body)
    {
        if (body.IsInGroup("player"))
        {
            var player = body as PlayerCharacter;
            player.AddHealth(HealthValue);
            VFXAnimationPlayer.Play("PlayParticle");
            Visual.Visible = false;
        }
    }
}
