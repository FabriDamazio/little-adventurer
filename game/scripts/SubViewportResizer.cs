using Godot;

public partial class SubViewportResizer : SubViewport
{
    public override void _Process(double delta)
    {
        Size = (Vector2I)GetNode<Label>("Label").GetRect().Size;
    }
}
