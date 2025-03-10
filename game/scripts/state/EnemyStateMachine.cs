using Godot;

public partial class EnemyStateMachine : Node
{
    [Export]
    public NodePath InitialState;

    public EnemyStateBase CurrentState;


    public override void _Ready()
    {
        CurrentState = GetNode(InitialState) as EnemyStateBase;

        foreach (var child in GetChildren())
        {
            var state = child as EnemyStateBase;
            state.StateMachine = this;
            state.CharacterBody3D = GetParent<Enemy>();
            state.AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayerEnemy");
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
        CurrentState = GetNode<EnemyStateBase>(targetState);
        CurrentState.Enter();

    }
}
