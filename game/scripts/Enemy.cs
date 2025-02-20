using Godot;

public partial class Enemy : CharacterBody3D
{
    public const float Speed = 2.0f;

    [Export]
    public PlayerCharacter Player;

    private NavigationAgent3D _navigationAgent3D;
    private Vector3 _direction;
    private float _stopDistance = 2.2f;

    public override void _Ready()
    {
        _navigationAgent3D = GetNode<NavigationAgent3D>("%NavigationAgent3D");
    }



    public override void _PhysicsProcess(double delta)
    {
        _navigationAgent3D.TargetPosition = Player.GlobalPosition;
        _direction = _navigationAgent3D.GetNextPathPosition() - GlobalPosition;
        _direction.Normalized();

        if (_navigationAgent3D.DistanceToTarget() < _stopDistance)
            return;

        Velocity = Velocity.Lerp(_direction * Speed, (float)delta);

        MoveAndSlide();
    }
}
