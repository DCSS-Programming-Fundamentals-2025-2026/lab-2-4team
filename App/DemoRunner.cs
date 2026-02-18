using Lab1.PlayingTable;
using Lab1.LogsClasses;
using Lab1.Players;

namespace Lab1.App
{
    public class DemoRunner
    {
        private List<NickName> playersList = new List<NickName>();
        private List<MatchInfo> matchInfo = new List<MatchInfo>();
        private Mechanic mechanic = new Mechanic();
        private int matchesCount = 0;

        public void Run()
        {
            Console.WriteLine("--------Welcome to Tic-Tac-Toe!--------");
            Console.WriteLine("Choose an option below:");
            while (true)
            {
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Matches Logs");
                Console.WriteLine("3. Leaderboard");
                Console.WriteLine("0. Exit");
                Console.WriteLine("------------------");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input, try again (0-...)");
                    Console.WriteLine("------------------");
                    continue;
                }
                else if (choice == 1)
                {
                    char[] board = new char[9];
                    int[] score = { 0, 0 };
                    Player[] players = new Player[2];
                    int SelectionIndex = 0;
                    Random rnd = new Random();
                    int PlayerTurn = rnd.Next(0, 2);

                    NewGame(players, board, ref SelectionIndex, PlayerTurn);

                    while (!mechanic.WinDraw(players, board, ref score))
                    {
                        mechanic.Turn(players, board, ref PlayerTurn, ref SelectionIndex);
                    }

                    ShowScore(players, score);
                    SaveMatchLogs(players, score, board);
                    Console.WriteLine("------------------");
                }
                else if (choice == 2)
                {
                    ShowMatchesHistory();
                }
                else if (choice == 3)
                {
                    ShowLeaderBoard();
                }
                else if (choice == 0)
                {
                    break;
                }
            }
        }

        private void NewGame(Player[] players, char[] board, ref int SelectionIndex, int playerTurnIndex)
        {
            players[0] = CreatePlayer(1);
            players[1] = CreatePlayer(2);

            IBoardRenderer obj = new Board();

            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '\0')
                {
                    SelectionIndex = i;
                    break;
                }
            }
            obj.DrawBoard(board, SelectionIndex, playerTurnIndex);
            ShowPlayerTurn(players, playerTurnIndex);
        }

        private Player CreatePlayer(int playerNumber)
        {
            Console.WriteLine($"Enter player{playerNumber} name: ");
            string name = Console.ReadLine();
            char symbol = '\0';
            bool nickIAIL = false;
            char usedSymbol = '\0';
            foreach (NickName savedPlayer in playersList)
            {
                if (savedPlayer.Name == name)
                {
                    usedSymbol = savedPlayer.Symbol;
                    nickIAIL = true;
                }
            }
            if (nickIAIL)
            {
                symbol = usedSymbol;
            }
            else
            {
                bool symbolIsValid = false;
                while (!symbolIsValid)
                {
                    Console.WriteLine($"Enter player{playerNumber}'s symbol: ");
                    while (!char.TryParse(Console.ReadLine(), out symbol))
                    {
                        Console.WriteLine("Write only one character");
                    }

                    bool symbolIsTaken = false;

                    foreach (NickName savedPlayer in playersList)
                    {
                        if (savedPlayer.Symbol == symbol)
                        {
                            symbolIsTaken = true;
                            break;
                        }
                    }

                    if (symbolIsTaken)
                    {
                        Console.WriteLine("Someone privatized this symbol :{ , choose another one");
                    }
                    else
                    {
                        symbolIsValid = true;
                    }
                }
            }
            return new (name, symbol);
        }

        private void ShowScore(Player[] players, int[] score)
        {
            Console.WriteLine();
            Console.WriteLine($"Player {players[0].Name}'s score: {score[0]}");
            Console.WriteLine($"Player {players[1].Name}'s score: {score[1]}");
        }

        private void ShowPlayerTurn(Player[] players, int playerTurnIndex)
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

        private void SaveMatchLogs(Player[] players, int[] score, char[] board)
        {
            for (int i = 0; i < players.Length; i++)
            {
                bool playerFound = false;

                foreach (NickName savedPlayer in playersList)
                {
                    if (players[i].Name == savedPlayer.Name && players[i].Symbol == savedPlayer.Symbol)
                    {
                        savedPlayer.Score += score[i];
                        playerFound = true;
                        break;
                    }
                }
                if (!playerFound)
                {
                    Console.WriteLine($"Adding new player: {players[i].Name} with symbol {players[i].Symbol}");
                    playersList.Add(new NickName(players[i].Name, players[i].Symbol, score[i]));
                    Console.WriteLine($"Players list count after adding: {playersList.Count}");
                }
            }

            char[] boardCopy = new char[board.Length];
            Array.Copy(board, boardCopy, board.Length);

            matchInfo.Add(new MatchInfo(players[0].Name, players[1].Name, players[0].Symbol, players[1].Symbol, score[0], score[1], boardCopy));

            matchesCount++;
        }

        private void ShowMatchesHistory()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"Total matches played - {matchesCount}");

            for (int i = 0; i < matchInfo.Count; i++)
            {
                MatchInfo match = matchInfo[i];
                Console.WriteLine($"Match #{i + 1}");
                Console.WriteLine($"{match.NameP1}:{match.SymbolP1}     {match.NameP2}:{match.SymbolP2}");
                DrawMatchBoard(match.Board);

                if (match.ScoreP1 > match.ScoreP2)
                {
                    Console.WriteLine($"Winner: {match.NameP1} (+1 point!)");
                }
                else if (match.ScoreP2 > match.ScoreP1)
                {
                    Console.WriteLine($"Winner: {match.NameP2} (+1 point!)");
                }
                else
                {
                    Console.WriteLine("Result: Draw");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------");
        }

        private void DrawMatchBoard(char[] board)
        {
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
        }

        private void ShowLeaderBoard()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"Nicks Amount - {playersList.Count}");
            int visualCounter = 1;
            foreach (NickName savedNicks in playersList)
            {
                Console.WriteLine($"{visualCounter}. {savedNicks.Name} - Players symbol: {savedNicks.Symbol}, Players score: {savedNicks.Score}");
                visualCounter++;
            }
            Console.WriteLine("------------------");
        }
    }
}
