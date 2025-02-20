using Godot;

public partial class StateBase : Node
{
    [Export]
    public string AnimationName = string.Empty;

    public StateMachine StateMachine;
    public CharacterBody3D CharacterBody3D;
    public AnimationPlayer AnimationPlayer;


    public override void _Ready()
    {
    }

    public void Enter()
    {
        GD.Print($"Entering state: {Name}");
        AnimationPlayer.Play(AnimationName);
    }

    public void Exit()
    {
        GD.Print($"Exiting state: {AnimationName}");
    }

    public void StateUpdate(double delta)
    {

    }

    public void ShowInfo()
    {
        GD.Print($"{Name} / Player:{CharacterBody3D} / StateMachine:{StateMachine}");
    }

}
