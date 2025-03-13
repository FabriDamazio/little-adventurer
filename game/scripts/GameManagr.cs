using Godot;

public partial class GameManagr : Node3D
{
    [Export]
    public TextureButton GameFinishRestartButton;

    [Export]
    public TextureButton GameOverRestartButton;

    [Export]
    public TextureButton GamePauseRestartButton;

    [Export]
    public TextureButton GameFinishMainMenuButton;

    [Export]
    public TextureButton GameOverMainMenuButton;

    [Export]
    public TextureButton GamePauseMainMenuButton;

    [Export]
    public TextureButton GamePauseResumeButton;

    public override void _Ready()
    {
        GameFinishRestartButton.ButtonUp += OnRestartButtonUp;
        GameOverRestartButton.ButtonUp += OnRestartButtonUp;
        GamePauseRestartButton.ButtonUp += OnRestartButtonUp;
        GameFinishMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GameOverMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GamePauseMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GamePauseResumeButton.ButtonUp += OnResumeButtonUp;
    }

    public void OnRestartButtonUp()
    {
        GD.Print("restart button");
    }

    public void OnMainMenuButtonUp()
    {
        GD.Print("main menu button");
    }

    public void OnResumeButtonUp()
    {
        GD.Print("resume button");
    }
}
