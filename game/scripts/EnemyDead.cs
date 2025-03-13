using Godot;

public partial class EnemyDead : EnemyStateBase
{
    [Export]
    public CollisionShape3D CollisionShape3D;

    [Export]
    public AnimationPlayer MaterialEffectAnimationPlayer;

    [Export]
    public PackedScene HealingOrb;

    [Signal]
    public delegate void EnemyDeathEventHandler(Enemy enemy);

    public override async void Enter()
    {
        base.Enter();
        CharacterBody3D.Velocity = Vector3.Zero;
        CollisionShape3D.Disabled = true;

        await ToSignal(GetTree().CreateTimer(1), "timeout");
        MaterialEffectAnimationPlayer.Play("dead");

        await ToSignal(GetTree().CreateTimer(3), "timeout");

        EmitSignal(SignalName.EnemyDeath, CharacterBody3D);
        CharacterBody3D.QueueFree();

        var dropItem = HealingOrb.Instantiate<CollectableHealingOrb>();
        GetTree().Root.GetNode("Node3D").AddChild(dropItem);
        dropItem.GlobalPosition = CharacterBody3D.GlobalPosition;

    }
}
