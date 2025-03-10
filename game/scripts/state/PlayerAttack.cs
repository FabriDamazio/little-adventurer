using Godot;

public partial class PlayerAttack : StateBase
{
    [Export]
    public CollisionShape3D HitBoxCollisionShape;

    [Export]
    public Area3D HitBox;

    [Export]
    public AnimationPlayer BladeMaterialEffectAnimationPlayer;

    [Export]
    public Node3D VFXBlade;

    [Export]
    public GpuParticles3D VFXHit;

    public int Damage = 40;
    public double SlideSpeed = 500;
    public double RemainSlideDuration;
    public Vector3 FacingDir;

    public override void _Ready()
    {
        HitBox.BodyEntered += OnHitBoxBodyEntered;
    }

    public void EnableHitBox()
    {
        HitBoxCollisionShape.Disabled = false;
        GD.Print("hitbox enabled");
    }

    public void DisableHitBox()
    {
        HitBoxCollisionShape.Disabled = true;
        GD.Print("hitbox disabled");
    }

    public override void Enter()
    {
        base.Enter();

        CharacterBody3D.Velocity = new Vector3(0, CharacterBody3D.Velocity.Y, 0);

        VFXBlade.Visible = true;
        BladeMaterialEffectAnimationPlayer.Stop();
        BladeMaterialEffectAnimationPlayer.Play("PlayBladeVFX");

        RemainSlideDuration = AnimationPlayer.CurrentAnimationLength * 0.3;
    }

    public override void Exit()
    {
        base.Exit();

        DisableHitBox();
        VFXBlade.Visible = false;
    }

    public override void StateUpdate(double delta)
    {
        RemainSlideDuration -= delta;
        FacingDir = CharacterBody3D.VisualNode.Transform.Basis.Z;

        if (RemainSlideDuration > 0)
        {
            var newVelocity = CharacterBody3D.Velocity;
            newVelocity.X = FacingDir.X * (float)SlideSpeed * (float)delta;
            newVelocity.Z = FacingDir.Z * (float)SlideSpeed * (float)delta;
            CharacterBody3D.Velocity = newVelocity;
        }
        else
        {
            var newVelocity = CharacterBody3D.Velocity;
            newVelocity.X = Mathf.MoveToward(CharacterBody3D.Velocity.X, 0, PlayerCharacter.Speed);
            newVelocity.Z = Mathf.MoveToward(CharacterBody3D.Velocity.Z, 0, PlayerCharacter.Speed);
            CharacterBody3D.Velocity = newVelocity;
        }

        if (AnimationPlayer.IsPlaying() == false)
        {
            StateMachine.SwitchTo("Idle");
        }
    }

    public void OnHitBoxBodyEntered(Node3D body)
    {
        if (body.IsInGroup("enemy"))
        {
            var enemy = body as Enemy;
            enemy.ApplyDamage(Damage);

            var position = body.GlobalPosition;
            position.Y = 1.5f;
            VFXHit.GlobalPosition = position;
            VFXHit.Restart();

            RemainSlideDuration = 0;
        }

    }

}
