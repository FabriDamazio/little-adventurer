using Godot;

public partial class PlayerRun : StateBase
{
    [Export]
    public GpuParticles3D FootStepsVFX;

    public override void StateUpdate(double delta)
    {
        Vector3 velocity = CharacterBody3D.Velocity;

        if (CharacterBody3D.Direction != Vector3.Zero)
        {
            velocity.X = CharacterBody3D.Direction.X * PlayerCharacter.Speed;
            velocity.Z = CharacterBody3D.Direction.Z * PlayerCharacter.Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, PlayerCharacter.Speed);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0, PlayerCharacter.Speed);
        }

        CharacterBody3D.Velocity = velocity;

        if (CharacterBody3D.Velocity.Length() > 0.2)
        {
            var lookDir = new Vector2(CharacterBody3D.Velocity.Z, CharacterBody3D.Velocity.X);
            CharacterBody3D.VisualNode.Rotation =
              new Vector3(
                  CharacterBody3D.VisualNode.Rotation.X,
                  lookDir.Angle(),
                  CharacterBody3D.VisualNode.Rotation.Z);
        }

        if (CharacterBody3D.Direction == Vector3.Zero)
        {
            StateMachine.SwitchTo("Idle");
        }

    }

    public override void Enter()
    {
        base.Enter();
        FootStepsVFX.Emitting = true;

    }

    public override void Exit()
    {
        base.Exit();
        FootStepsVFX.Emitting = false;
    }
}
