using Godot;

public partial class EnemyChasePlayer : EnemyStateBase
{
    private float _stopDistance = 2.2f;

    public override void StateUpdate(double delta)
    {
        base.StateUpdate(delta);
        CharacterBody3D.NavigationAgent3D.TargetPosition = CharacterBody3D.Player.GlobalPosition;
        CharacterBody3D.Direction =
          CharacterBody3D.NavigationAgent3D.GetNextPathPosition() - CharacterBody3D.GlobalPosition;

        CharacterBody3D.Direction.Normalized();

        CharacterBody3D.Velocity =
          CharacterBody3D.Velocity.Lerp(CharacterBody3D.Direction * PlayerCharacter.Speed, (float)delta);

        if (CharacterBody3D.Velocity.Length() > 0.2)
        {
            var lookDir = new Vector2(CharacterBody3D.Velocity.Z, CharacterBody3D.Velocity.X);
            CharacterBody3D.VisualNode.Rotation =
              new Vector3(
                  CharacterBody3D.VisualNode.Rotation.X,
                  lookDir.Angle(),
                  CharacterBody3D.VisualNode.Rotation.Z);
        }

        if (CharacterBody3D.NavigationAgent3D.DistanceToTarget() < _stopDistance)
        {
            StateMachine.SwitchTo("Attack");
        }
    }
}
