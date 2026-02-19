namespace Lab1.LogsClasses
{
    public class MatchInfo
    {
        public string NameP1;
        public string NameP2;
        public char SymbolP1;
        public char SymbolP2;
        public int ScoreP1;
        public int ScoreP2;
        public char[] Board;

        public MatchInfo(string namep1, string namep2, char symbolp1, char symbolp2, int scorep1, int scorep2, char[] board)
        {
            NameP1 = namep1;
            NameP2 = namep2;
            SymbolP1 = symbolp1;
            SymbolP2 = symbolp2;
            ScoreP1 = scorep1;
            ScoreP2 = scorep2;
            Board = board;
        }
    }
}
