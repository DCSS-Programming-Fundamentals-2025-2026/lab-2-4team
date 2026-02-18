namespace Lab1.PlayingTable;
public class Board : IBoardRenderer
{   
    public void DrawBoard (char[] board, int SelectionIndex, int PlayerTurn)
    {
        int n = (int)Math.Sqrt(board.Length);

        for(int i = 0; i < n; i++)
        {
            Console.WriteLine("---------");
            for(int j = 0; j < n; j++)
            {
                int index = (i * n) + j;

                if(index == SelectionIndex && PlayerTurn == 1)
                {
                    Console.Write("[/]");
                }
                else if(index == SelectionIndex && PlayerTurn == 0)
                {
                    Console.Write("[0]");
                }
                else if(board[index] == '\0')
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
    }
}