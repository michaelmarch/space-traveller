namespace SpaceTraveller;

using Godot;

public class Portal2D : Area2D
{
    [Export]
    private PackedScene nextScene;

    public void OnBodyEntered(PhysicsBody2D body)
    {
        Teleport();
    }

    public override string _GetConfigurationWarning()
    {
        return (nextScene == null) ? "The property Next Level can't be empty" : "";
    }


    public async void Teleport()
    {
        GetTree().Paused = true;

        var animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animPlayer.Play("fade_out");

        await ToSignal(animPlayer, "animation_finished");

        GetTree().Paused = false;
        GetTree().ChangeSceneTo(nextScene);
    }
}
