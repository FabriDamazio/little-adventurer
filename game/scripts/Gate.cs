using Godot;

public partial class Gate : Node3D
{
    [Export]
    public AnimationPlayer AnimationPlayer;

    public void Open()
    {
        AnimationPlayer.Play("open");
    }
}
