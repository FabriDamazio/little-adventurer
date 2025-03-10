using Godot;

public partial class EnemySpawn : EnemyStateBase
{
    [Export]
    public AnimationPlayer MaterialEffectAnimationPlayer;

    public double SpawnDuration = 1.5;

    public override void Enter()
    {
        base.Enter();
        MaterialEffectAnimationPlayer.Play("spawn");
    }

    public override void StateUpdate(double delta)
    {
        SpawnDuration -= delta;

        if (SpawnDuration <= 0)
        {
            StateMachine.SwitchTo("ChasePlayer");
        }
    }
}
