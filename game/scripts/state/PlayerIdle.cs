using Godot;

public partial class PlayerIdle : StateBase
{
    public override void StateUpdate(double delta)
    {
        if (CharacterBody3D.SlideKeyPressed)
        {
            StateMachine.SwitchTo("Slide");
        }

        if (CharacterBody3D.Direction != Vector3.Zero)
        {
            StateMachine.SwitchTo("Run");
        }

    }
}
