using Godot;

public partial class EnemyDead : EnemyStateBase
{
    [Export]
    public CollisionShape3D CollisionShape3D;

    [Export]
    public AnimationPlayer MaterialEffectAnimationPlayer;

    public override async void Enter()
    {
        base.Enter();
        CharacterBody3D.Velocity = Vector3.Zero;
        CollisionShape3D.Disabled = true;

        await ToSignal(GetTree().CreateTimer(1), "timeout");
        MaterialEffectAnimationPlayer.Play("dead");

        await ToSignal(GetTree().CreateTimer(3), "timeout");

        CharacterBody3D.QueueFree();
    }
}
