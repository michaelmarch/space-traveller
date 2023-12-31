namespace SpaceTraveller;

using Godot;

public class PlayerData : Node
{
    [Signal]
    public delegate void Updated();
    [Signal]
    public delegate void Died();
    [Signal]
    public delegate void Reset();

    private static PlayerData instance;

    private PlayerData()
    {
        instance = this;
    }

    public static PlayerData Instance => instance;

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            EmitSignal(nameof(Updated));
            score = value;
        }
    }

    private int deaths = 0;

    public int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            EmitSignal(nameof(Died));
            deaths = value;
        }
    }

}
