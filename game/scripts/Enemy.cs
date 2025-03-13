using Godot;

public partial class Enemy : CharacterBody3D
{
    public const float Speed = 1.0f;
    public PlayerCharacter Player;

    [Export]
    public AnimationPlayer MaterialEffectAnimationPlayer;

    [Export]
    public EnemyDead DeadState;

    public int MaxHealth = 100;
    public int CurrentHealth;

    public NavigationAgent3D NavigationAgent3D;
    public Node3D VisualNode;
    public AnimationPlayer AnimationPlayer;
    public Vector3 Direction;

    public override void _Ready()
    {
        NavigationAgent3D = GetNode<NavigationAgent3D>("%NavigationAgent3D");
        VisualNode = GetNode<Node3D>("%VisualNodeEnemy");
        AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayerEnemy");
        CurrentHealth = MaxHealth;
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        Player = GetTree().Root.GetNode<PlayerCharacter>("Node3D/Player");
    }


    public override void _PhysicsProcess(double delta)
    {
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
