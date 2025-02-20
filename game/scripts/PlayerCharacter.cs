using Godot;

public partial class PlayerCharacter : CharacterBody3D
{
    public const float Speed = 5.0f;
    public int CoinNumber = 0;

    [Signal]
    public delegate void CoinNumberUpdatedEventHandler(int value);

    private Node3D _visualNode;
    private AnimationPlayer _animationPlayer;
    private GpuParticles3D _footStepVFX;

    public override void _Ready()
    {
        _visualNode = GetNode<Node3D>("%VisualNode");
        _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
        _footStepVFX = GetNode<GpuParticles3D>("%Footstep_GPUParticles3D");
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
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
            _animationPlayer.Play("LittleAdventurerAndie_Run");
            _footStepVFX.Emitting = true;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
            _animationPlayer.Play("LittleAdventurerAndie_Idel");
            _footStepVFX.Emitting = false;
        }

        Velocity = velocity;

        if (Velocity.Length() > 0.2)
        {
            var lookDir = new Vector2(Velocity.Z, Velocity.X);
            _visualNode.Rotation =
              new Vector3(_visualNode.Rotation.X, lookDir.Angle(), _visualNode.Rotation.Z);
        }

        MoveAndSlide();
    }

    public void AddCoin(int value)
    {
        CoinNumber += value;
        EmitSignal(SignalName.CoinNumberUpdated, CoinNumber);
        GD.Print(CoinNumber);
    }
}
