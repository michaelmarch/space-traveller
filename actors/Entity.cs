namespace SpaceTraveller;

using Godot;

public abstract class Entity : KinematicBody2D
{
    [Export]
    protected Vector2 speed = new Vector2(400.0f, 500.0f);

    [Export]
    private float gravity = 3500.0f;

    protected Vector2 floorNormal = Vector2.Up;

    protected Vector2 _velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        _velocity.y += gravity * delta;
    }
}
