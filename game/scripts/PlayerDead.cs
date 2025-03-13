using Godot;

public partial class PlayerDead : StateBase
{
    public override async void Enter()
    {
        base.Enter();

        StateMachine.GetParent().RemoveFromGroup("player");

        await ToSignal(GetTree().CreateTimer(2), SceneTreeTimer.SignalName.Timeout);

        CharacterBody3D.IsDead = true;
    }
}
