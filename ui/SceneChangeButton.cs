namespace SpaceTraveller;

using Godot;

public class SceneChangeButton : Button
{
    public void OnSceneChangeButtonUp()
    {
        GetTree().ChangeScene("res://levels/level01.tscn");
    }
}
