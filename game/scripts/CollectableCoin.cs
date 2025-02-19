using Godot;

public partial class CollectableCoin : Node3D
{
    private Node3D _visualNode;
    private Area3D _area3D;
    private GpuParticles3D _pickupVFX;
    private int RotateSpeed = 1;

    public override void _Ready()
    {
        _visualNode = GetNode<Node3D>("%VisualNodeCoin");
        _area3D = GetNode<Area3D>("%CoinArea3D");
        _pickupVFX = GetNode<GpuParticles3D>("%CoinVFXNode");
        _area3D.BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
        _visualNode.RotateY((float)(RotateSpeed * delta));

        if (!_visualNode.Visible && _pickupVFX.Emitting == false)
            QueueFree();
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body.IsInGroup("player"))
        {
            _pickupVFX.Emitting = true;
            _visualNode.Visible = false;
        }
    }
}
