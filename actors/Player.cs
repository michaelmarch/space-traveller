namespace SpaceTraveller;

using Godot;

public class Player : Entity
{
    [Export]
    private float stompImpulse = 600.0F;


    private AnimatedSprite sprite;


    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite>("Sprite");
    }

    public void OnStompDetectorAreaEntered(Area2D area)
    {
        _velocity = CalculateStompVelocity(_velocity, stompImpulse);
    }

    public void OnEnemyDetectorBodyEntered(PhysicsBody2D body)
    {
        Die();
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        var isJumpInterrupted = Input.IsActionJustReleased("jump") && _velocity.y < 0.0F;

        var direction = GetDirection();
        _velocity = CalculateMoveVelocity(_velocity, direction, speed, isJumpInterrupted);

        var snap = (direction.y == 0.0) ? (60 * Vector2.Down) : Vector2.Zero;
        _velocity = MoveAndSlideWithSnap(
            _velocity, snap, floorNormal, true
        );
    }

    public Vector2 GetDirection()
    {
        float x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

        if (x < 0)
        {
            sprite.Play();
            sprite.FlipH = false;
        }
        else if (x > 0)
        {
            sprite.Play();
            sprite.FlipH = true;
        }
        else
        {
            sprite.Stop();
            sprite.Frame = 1;
        }
        float y = 0;

        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            y = -Input.GetActionStrength("jump");
        }

        return new Vector2(x, y);
    }

    public Vector2 CalculateMoveVelocity(
        Vector2 linearVelocity,
        Vector2 direction,
        Vector2 speed,
        bool isJumpInterrupted
    )
    {
        var velocity = linearVelocity;
        velocity.x = speed.x * direction.x;

        if (direction.y != 0.0)
        {
            velocity.y = speed.y * direction.y;
        }

        if (isJumpInterrupted)
        {
            velocity.y = 0.0F;
        }

        return velocity;
    }

    private Vector2 CalculateStompVelocity(Vector2 linearVelocity, float stompImpulse)
    {
        var stompJump = Input.IsActionJustPressed("jump") ? -speed.y : -stompImpulse;

        return new Vector2(linearVelocity.x, stompJump);
    }


    private void Die()
    {
        PlayerData.Instance.Deaths += 1;
        QueueFree();
    }

}
