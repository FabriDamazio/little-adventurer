using Godot;

public partial class PlayerSlide : StateBase

{
    public float SlideSpeed = 650.0f;
    public float SlideDuration = 0.3f;
    public float RemainSlideDuration;
    public Vector3 FacingDir;

    public override void Enter()
    {
        base.Enter();
        CharacterBody3D.Velocity = new Vector3(0, 0, CharacterBody3D.Velocity.Z);
        RemainSlideDuration = SlideDuration;
    }

    public override void StateUpdate(double delta)
    {
        base.StateUpdate(delta);

        RemainSlideDuration -= (float)delta;
        FacingDir = CharacterBody3D.VisualNode.Transform.Basis.Z;

        if (RemainSlideDuration > 0)
        {
            CharacterBody3D.Velocity =
              new Vector3(
                  FacingDir.X * SlideSpeed * (float)delta,
                  FacingDir.Y * SlideSpeed * (float)delta,
                  CharacterBody3D.Velocity.Z);
        }
        else
        {
            CharacterBody3D.Velocity = Vector3.Zero;
            StateMachine.SwitchTo("Idle");
        }

    }
}
