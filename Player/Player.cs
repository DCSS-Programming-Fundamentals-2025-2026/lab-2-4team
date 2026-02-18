namespace Lab1.Players;

public class Player : PlayerBase
{
    public Player(string name, char symbol) : base(name, symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    public void PlayerTurn(Player[] players, int playerTurnIndex)
    {
        if (playerTurnIndex == 0)
        {
            Console.WriteLine($"Now it's {players[0].Name}`s turn!");
        }
        else if (playerTurnIndex == 1)
        {
            Console.WriteLine($"Now it's {players[1].Name}`s turn!");
        }
    }
}