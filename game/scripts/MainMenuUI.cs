using Godot;

public partial class MainMenuUI : Control
{
    [Export]
    public TextureButton StartButton;

    [Export]
    public TextureButton QuitButton;

    public override void _Ready()
    {
        StartButton.ButtonUp += OnStartButtonUp;
        QuitButton.ButtonUp += OnQuitButtonUp;
    }

    public void OnStartButtonUp()
    {
        GetTree().ChangeSceneToFile("res://game/scenes/game_level.tscn");
    }

    public void OnQuitButtonUp()
    {
        GetTree().Quit();
    }
}
