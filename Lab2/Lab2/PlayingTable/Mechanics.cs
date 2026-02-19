namespace Lab1.PlayingTable;

using Lab1.Players;

public class Mechanic
{
    private Player playerHelper = new Player("", '\0');

    public void Turn(Player[] players, char[] board, ref int PlayerTurn, ref int SelectionIndex)
    {
        SafeClear();
        IBoardRenderer obj = new Board();

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);
        playerHelper.PlayerTurn(players, PlayerTurn);

        bool start = true;
        while (start)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (SelectionIndex - 3 < 0)
                    { 
                        continue; 
                    }
                    SafeClear();
                    TurnWithDecrement(players, board, PlayerTurn, ref SelectionIndex, 3);
                    break;

                case ConsoleKey.DownArrow:
                    if (SelectionIndex + 3 > 8)
                    { 
                        continue; 
                    }
                    SafeClear();
                    TurnWithIncrement(players, board, PlayerTurn, ref SelectionIndex, 3);
                    break;

                case ConsoleKey.RightArrow:
                    if (SelectionIndex + 1 > 8)
                    { 
                        continue; 
                    }
                    SafeClear();
                    TurnWithIncrement(players, board, PlayerTurn, ref SelectionIndex, 1);
                    break;

                case ConsoleKey.LeftArrow:
                    if (SelectionIndex - 1 < 0)
                    { 
                        continue; 
                    }
                    SafeClear();
                    TurnWithDecrement(players, board, PlayerTurn, ref SelectionIndex, 1);
                    break;

                case ConsoleKey.Enter:
                    if (PlayerTurn == 0 && board[SelectionIndex] == 0)
                    {
                        SafeClear();
                        TurnChoose(players, board, ref PlayerTurn, ref SelectionIndex, ref start, 0, 1);
                    }
                    else if (PlayerTurn == 1 && board[SelectionIndex] == 0)
                    {
                        SafeClear();
                        TurnChoose(players, board, ref PlayerTurn, ref SelectionIndex, ref start, 1, 0);
                    }
                    else
                    {
                        Console.WriteLine("Клітинка зайнята. Оберіть іншу");
                    }
                    break;

                case ConsoleKey.Escape:
                    start = false;
                    break;
            }
        }
    }

    public void TurnWithDecrement(Player[] players, char[] board, int PlayerTurn, ref int SelectionIndex, int step)
    {
        IBoardRenderer obj = new Board();

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);

        SafeClear();
        SelectionIndex -= step;

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);
        playerHelper.PlayerTurn(players, PlayerTurn);
    }

    public void TurnWithIncrement(Player[] players, char[] board, int PlayerTurn, ref int SelectionIndex, int step)
    {
        IBoardRenderer obj = new Board();

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);

        SafeClear();
        SelectionIndex += step;

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);
        playerHelper.PlayerTurn(players, PlayerTurn);
    }

    public void TurnChoose(Player[] players, char[] board, ref int PlayerTurn, ref int SelectionIndex, ref bool start, int index, int index2)
    {
        IBoardRenderer obj = new Board();

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);

        SafeClear();
        board[SelectionIndex] = players[index].Symbol;

        obj.DrawBoard(board, SelectionIndex, PlayerTurn);
        playerHelper.PlayerTurn(players, PlayerTurn);

        PlayerTurn = index2;
        start = false;
    }

    public bool WinDraw(Player[] players, char[] board, ref int[] score)
    {
        bool HasSpace = false;

        for (int i = 0; i < 9; i++)
        {
            if (i % 3 == 0 && board[i] == board[i + 1] && board[i] == board[i + 2] && board[i] != '\0')
            {
                DrawFinalBoard(board);
                PlayerSymbolCheck(players, board[i], ref score);
                return true;
            }

            else if (i + 6 < 9 && board[i] == board[i + 3] && board[i] == board[i + 6] && board[i] != '\0')
            {
                DrawFinalBoard(board);
                PlayerSymbolCheck(players, board[i], ref score);
                return true;
            }

            else if (i == 0 && board[i] == board[i + 4] && board[i] == board[i + 8] && board[i] != '\0')
            {
                DrawFinalBoard(board);
                PlayerSymbolCheck(players, board[i], ref score);
                return true;
            }

            else if (i == 2 && board[i] == board[i + 2] && board[i] == board[i + 4] && board[i] != '\0')
            {
                DrawFinalBoard(board);
                PlayerSymbolCheck(players, board[i], ref score);
                return true;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == '\0' || board[i] == '/') HasSpace = true;
        }

        if (!HasSpace)
        {
            DrawFinalBoard(board);
            Console.WriteLine("Draw!");
            return true;
        }
        return false;
    }

    public void PlayerSymbolCheck(Player[] players, char symbol, ref int[] score)
    {
        if (players[0].Symbol == symbol)
        {
            Console.WriteLine($"Player {players[0].Name} won!");
            score[0]++;
        }
        else
        {
            Console.WriteLine($"Player {players[1].Name} won!");
            score[1]++;
        }
    }

    public void DrawFinalBoard(char[] board)
    {
        SafeClear();
        int n = (int)Math.Sqrt(board.Length);

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("---------");
            for (int j = 0; j < n; j++)
            {
                int index = (i * n) + j;

                if (board[index] == '\0')
                {
                    Console.Write("[ ]");
                }
                else
                {
                    Console.Write($"[{board[index]}]");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------");
        Console.WriteLine();
    }

    private void SafeClear()
    {
        try 
        { 
            Console.Clear(); 
        } 
        catch (IOException)
        {
            
        }
    }
}