using Godot;

public partial class PlayerHurt : StateBase
{
    public Vector3 PushBackDir;
    public float PushBackSpeed = 50;

    public override void Enter()
    {
        base.Enter();

        CharacterBody3D.Velocity = Vector3.Zero;
    }

    public override void StateUpdate(double delta)
    {
        CharacterBody3D.Velocity = PushBackDir * PushBackSpeed * (float)delta;

        if (AnimationPlayer.IsPlaying() == false)
        {
            StateMachine.SwitchTo("Idle");
        }
    }

    public override void Exit()
    {
        base.Exit();

        CharacterBody3D.Velocity = Vector3.Zero;
    }
}
