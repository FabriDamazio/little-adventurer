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

    public int Damage = 40;

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
    }

    public override void Exit()
    {
        base.Exit();

        DisableHitBox();
        VFXBlade.Visible = false;
    }

    public override void StateUpdate(double delta)
    {
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
        }
    }

}
