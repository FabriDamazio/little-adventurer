using Godot;

public partial class Enemy : CharacterBody3D
{
    public const float Speed = 1.0f;

    [Export]
    public PlayerCharacter Player;

    [Export]
      public AnimationPlayer MaterialEffectAnimationPlayer;

    public int MaxHealth = 100;
    public int CurrentHealth;

    private NavigationAgent3D _navigationAgent3D;
    private Node3D _visualNode;
    private AnimationPlayer _animationPlayer;
    private Vector3 _direction;
    private float _stopDistance = 2.2f;

    public override void _Ready()
    {
        _navigationAgent3D = GetNode<NavigationAgent3D>("%NavigationAgent3D");
        _visualNode = GetNode<Node3D>("%VisualNodeEnemy");
        _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayerEnemy");
        CurrentHealth = MaxHealth;
    }



    public override void _PhysicsProcess(double delta)
    {
        _navigationAgent3D.TargetPosition = Player.GlobalPosition;
        _direction = _navigationAgent3D.GetNextPathPosition() - GlobalPosition;
        _direction.Normalized();

        if (_navigationAgent3D.DistanceToTarget() < _stopDistance)
        {
            _animationPlayer.Play("NPC_01_IDEL");
            return;
        }

        Velocity = Velocity.Lerp(_direction * Speed, (float)delta);
        _animationPlayer.Play("NPC_01_WALK");

        if (Velocity.Length() > 0.2)
        {
            var lookDir = new Vector2(Velocity.Z, Velocity.X);
            _visualNode.Rotation =
              new Vector3(_visualNode.Rotation.X, lookDir.Angle(), _visualNode.Rotation.Z);
        }

        MoveAndSlide();
    }

    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        GD.Print($"{Name} health: {CurrentHealth}");
        MaterialEffectAnimationPlayer.Play("flash");
    }
}
