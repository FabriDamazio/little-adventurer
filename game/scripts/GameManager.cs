using Godot;

public partial class GameManager : Node3D
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

    [Export]
    public PlayerCharacter Player;

    [Export]
    public GameplayUIManager GameplayUIManager;

    [Export]
    public Portal Portal;

    private bool _isPaused;

    public bool IsPaused
    {
        get { return _isPaused; }

        set
        {
            _isPaused = value;
            GameplayUIManager.TogglePauseUI(IsPaused);
            GetTree().Paused = IsPaused;
        }
    }

    public override void _Ready()
    {
        GameFinishRestartButton.ButtonUp += OnRestartButtonUp;
        GameOverRestartButton.ButtonUp += OnRestartButtonUp;
        GamePauseRestartButton.ButtonUp += OnRestartButtonUp;
        GameFinishMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GameOverMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GamePauseMainMenuButton.ButtonUp += OnMainMenuButtonUp;
        GamePauseResumeButton.ButtonUp += OnResumeButtonUp;

        Portal.PlayerReachThePortal += GameFinished;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("pause"))
        {
            IsPaused = true;
        }

        if (Player.IsDead)
            GameOver();
    }

    public void GameFinished()
    {
        GetTree().Paused = true;
        GameplayUIManager.ToggleGameFinishUI(true);
    }

    public void GameOver()
    {
        GetTree().Paused = true;
        GameplayUIManager.ToggleGameFinishUI(true);
    }

    public void OnRestartButtonUp()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    public void OnMainMenuButtonUp()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://game/scenes/MainScene.tscn");

    }

    public void OnResumeButtonUp()
    {
        IsPaused = false;
    }
}
