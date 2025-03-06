using Godot;

public partial class PlayerAttack : StateBase
{
    [Export]
    public CollisionShape3D HitBoxCollisionShape;

    public void EnableHitBox()
    {
        HitBoxCollisionShape.Disabled = false;
    }

    public void DisableHitBox()
    {
        HitBoxCollisionShape.Disabled = true;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterBody3D.Velocity = new Vector3(0, CharacterBody3D.Velocity.Y, 0);
    }

    public override void Exit()
    {
        base.Exit();

        DisableHitBox();
    }

    public override void StateUpdate(double delta)
    {
        if (AnimationPlayer.IsPlaying() == false)
        {
            StateMachine.SwitchTo("Idle");
        }
    }
}
