using Godot;

public partial class StateMachine : Node
{
    [Export]
    public NodePath InitialState;

    public StateBase CurrentState;


    public override void _Ready()
    {
        CurrentState = GetNode(InitialState) as StateBase;

        foreach (var child in GetChildren())
        {
            var state = child as StateBase;
            state.StateMachine = this;
            state.CharacterBody3D = GetParent<CharacterBody3D>();
            state.AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
            state.ShowInfo();
        }

        CurrentState.Enter();
    }

    public override void _PhysicsProcess(double delta)
    {
        CurrentState.StateUpdate(delta);
    }

    public void SwitchTo(string targetState)
    {
        if (!HasNode(targetState))
        {
            GD.Print("Could not find the target state node");
            return;
        }

        CurrentState.Exit();
        CurrentState = GetNode<StateBase>(targetState);
        CurrentState.Enter();

    }
}
