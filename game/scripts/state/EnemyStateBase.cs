using Godot;

public partial class EnemyStateBase : Node
{
    [Export]
    public string AnimationName = string.Empty;

    public EnemyStateMachine StateMachine;
    public Enemy CharacterBody3D;
    public AnimationPlayer AnimationPlayer;


    public override void _Ready()
    {
    }

    public virtual void Enter()
    {
        GD.Print($"Entering state: {Name}");
        AnimationPlayer.Play(AnimationName);
    }

    public virtual void Exit()
    {
        GD.Print($"Exiting state: {AnimationName}");
    }

    public virtual void StateUpdate(double delta)
    {

    }

    public void ShowInfo()
    {
        GD.Print($"{Name} / Player:{CharacterBody3D} / StateMachine:{StateMachine}");
    }

}
