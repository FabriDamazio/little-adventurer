using Godot;

public partial class PlayerDead : StateBase
{
    public override void Enter()
    {
        base.Enter();

        StateMachine.GetParent().RemoveFromGroup("player");
    }
}
