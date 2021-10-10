namespace SpaceTraveller;

using System;
using Godot;

public class UserInterface : Node
{

    private bool Paused
    {
        get
        {
            return Paused;
        }
        set
        {
            GetTree().Paused = value;
            var pauseOverlay = GetNode<ColorRect>("PauseOverlay");
            pauseOverlay.Visible = value;
        }
    }

    public override void _Ready()
    {
        PlayerData.Instance.Connect("Updated", this, "UpdateInterface");
        PlayerData.Instance.Connect("Died", this, "OnPlayerDied");
        PlayerData.Instance.Connect("Reset", this, "OnPlayerReset");

        UpdateInterface();
    }

    public void OnPlayerDied()
    {
        Paused = true;
        GetNode<ColorRect>("PauseOverlay").GetNode<Label>("Title").Text = "You died";
    }

    public void OnPlayerReset()
    {
        Paused = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionPressed("pause"))
        {
            Paused = !Paused;
        }
    }
    public void UpdateInterface()
    {
        var score_label = GetNode<Label>("Score");
        score_label.Text = String.Format("Score: {0}", PlayerData.Instance.Score);

    }
}
