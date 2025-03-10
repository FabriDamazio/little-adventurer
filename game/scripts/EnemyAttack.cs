using Godot;

public partial class EnemyAttack : EnemyStateBase
{
    [Export]
    public CollisionShape3D HitBoxCollisionShape;

    [Export]
    public Area3D HitBox;

    [Export]
    public AnimationPlayer VFXAnimationPlayer;

    public int Damage = 30;
    public Vector3 lookDir;
    public Vector2 lookDir2D;

    public override void _Ready()
    {
        HitBox.BodyEntered += OnHitBoxBodyEntered;
    }

    public void EnableHitBox()
    {
        HitBoxCollisionShape.Disabled = false;
    }

    public void DisableHitBox()
    {
        HitBoxCollisionShape.Disabled = true;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterBody3D.Velocity = Vector3.Zero;

        lookDir = CharacterBody3D.Player.GlobalPosition - CharacterBody3D.GlobalPosition;
        lookDir2D = new Vector2(lookDir.Z, lookDir.X);
        var rotation = CharacterBody3D.VisualNode.Rotation;
        rotation.Y = lookDir2D.Angle();
        CharacterBody3D.VisualNode.Rotation = rotation;
    }

    public override void Exit()
    {
        base.Exit();

        DisableHitBox();
    }

    public override void StateUpdate(double delta)
    {
        base.StateUpdate(delta);

        if (AnimationPlayer.IsPlaying() == false)
        {
            StateMachine.SwitchTo("ChasePlayer");
        }

        if (CharacterBody3D.CurrentHealth == 0)
        {
            StateMachine.SwitchTo("Dead");
        }
    }

    public void OnHitBoxBodyEntered(Node3D body)
    {
        if (body.IsInGroup("player"))
        {
            var player = body as PlayerCharacter;
            player.TakeDamage(Damage);
        }
    }

    public void PlaySmashVFX()
    {
        VFXAnimationPlayer.Play("PlayParticle");
    }
}
