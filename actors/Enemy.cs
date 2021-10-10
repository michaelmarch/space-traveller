namespace SpaceTraveller;

using Godot;

public class Enemy : Entity
{
    [Export]
    private int score = 100;

    public override void _Ready()
    {
        SetPhysicsProcess(false);

        _velocity.x = -speed.x;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        _velocity.x *= IsOnWall() ? -1 : 1;
        _velocity.y = MoveAndSlide(_velocity, floorNormal).y;
    }


    public void OnStompArea2DAreaEntered(Area2D area)
    {
        var stompArea = GetNode<Area2D>("StompArea2D");

        if (area.GlobalPosition.y > stompArea.GlobalPosition.y)
        {
            return;
        }

        Die();
    }


    public void Die()
    {
        PlayerData.Instance.Score += score;
        QueueFree();
    }
}
