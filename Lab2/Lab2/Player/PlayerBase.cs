namespace Lab1.Players;

public abstract class PlayerBase
{
    public string Name { get; set; }
    public char Symbol { get; set; }

    public PlayerBase(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}