using Lab1.Players;

namespace Lab1.LogsClasses
{
    public class NickName : PlayerBase
    {
        public int Score;

        public NickName(string name, char symbol, int score) : base (name, symbol)
        {
            Score = score;
        }
    }
}