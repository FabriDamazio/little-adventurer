using Godot;

public partial class PlayerCharacter : CharacterBody3D
{
    public const float Speed = 5.0f;
    public int CoinNumber = 0;
    public Vector3 Direction;
    public bool SlideKeyPressed;
    public bool AttackKeyPressed;

    [Signal]
    public delegate void CoinNumberUpdatedEventHandler(int value);

    public Node3D VisualNode;
    public AnimationPlayer AnimationPlayer;
    public GpuParticles3D FootStepVFX;

    public override void _Ready()
    {
        VisualNode = GetNode<Node3D>("%VisualNode");
        AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
        FootStepVFX = GetNode<GpuParticles3D>("%Footstep_GPUParticles3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        SlideKeyPressed = Input.IsActionJustPressed("slide");
        AttackKeyPressed = Input.IsActionJustPressed("attack");

        MoveAndSlide();
    }

    public void AddCoin(int value)
    {
        CoinNumber += value;
        EmitSignal(SignalName.CoinNumberUpdated, CoinNumber);
        GD.Print(CoinNumber);
    }
}
