namespace SpaceTraveller;

using Godot;

public class Coin : Area2D
{
    [Export]
    private int score = 100;


    public void OnBodyEntered(PhysicsBody2D body)
    {
        Picked();
    }


    public void Picked()
    {
        PlayerData.Instance.Score += score;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("picked");
    }
}
