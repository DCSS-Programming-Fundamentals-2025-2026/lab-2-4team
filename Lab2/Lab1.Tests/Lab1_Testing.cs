using Lab1.App;
using Lab1.Players;
using Lab1.PlayingTable;
using NUnit.Framework;
using System;

namespace Lab1.Tests
{
    [TestFixture]
    public class TicTacToeTests
    {
        private Mechanic _mechanic;
        private DemoRunner _runner;
        private Player[] _players;
        private char[] _board;
        private int[] _score;

        [SetUp]
        public void Setup()
        {
            _mechanic = new Mechanic();
            _runner = new DemoRunner();
            _players =  
            [
                new Player("Dima", 'X'), 
                new Player("Arsen", 'O') 
            ];
            _board = new char[9]; 
            _score = [0, 0];
        }

        [TestCase(0, 1, 2)]
        [TestCase(3, 4, 5)]
        [TestCase(0, 4, 8)]
        [TestCase(1, 4, 7)]
        public void WinDraw_Player1Wins_IncrementsScore_ReturnsTrue(int a, int b, int c)
        {
            _board[a] = 'X';
            _board[b] = 'X';
            _board[c] = 'X'; 

            bool isGameOver = _mechanic.WinDraw(_players, _board, ref _score);

            Assert.That(isGameOver, Is.True, "Гра мала закінчитись");
            Assert.That(_score[0], Is.EqualTo(1), "Рахунок Гравця 1 мав стати 1");  
            Assert.That(_score[1], Is.EqualTo(0), "Рахунок Гравця 2 не мав змінитись");
        }

        [TestCase(99)]
        [TestCase(20)]
        [TestCase(-67)]
        [TestCase(67)]
        public void TurnChoose_IndexOutOfBounds_ThrowsException_KeepsState(int a)
        {
            int playerTurn = 0;
            int badSelectionIndex = a; 
            bool start = true;

            Assert.Throws<IndexOutOfRangeException>(() =>
                _mechanic.TurnChoose(_players, _board, ref playerTurn, ref badSelectionIndex, ref start, 0, 1),
            "Мав викинути помилку з виходом за межі");

            Assert.That(playerTurn, Is.EqualTo(0), "Черга мав залишитись колишньою");
            Assert.That(start, Is.True, "Цикл ходу не мав перерватися");
        }

        [Test]
        public void SaveMatchLogs_OverLimit_ShiftsArray_RemovesOldest()
        {
            for (int i = 0; i < 5; i++)
            {
                Player[] dummyPlayers = { new Player($"OldGuy{i}", 'X'), new Player("Bot", 'O') };
                _runner.SaveMatchLogs(dummyPlayers, _score, _board);
            }

            Player[] newPlayers = { new Player("NewGuy", 'X'), new Player("Bot", 'O') };

            _runner.SaveMatchLogs(newPlayers, _score, _board);

            Assert.That(_runner.historyCount, Is.EqualTo(5), "Кількість логів мала залишитись 5");
            Assert.That(_runner.matchInfo[0].NameP1, Is.Not.EqualTo("OldGuy0"), "OldGuy0 мав бути перезаписаний зсувом");
            Assert.That(_runner.matchInfo[0].NameP1, Is.EqualTo("OldGuy1"), "На індексі 0 тепер має бути OldGuy1");
            Assert.That(_runner.matchInfo[4].NameP1, Is.EqualTo("NewGuy"), "NewGuy має бути на останньому індексі 4");
        }

        [TestCase(0, 0, 4)]
        [TestCase(0, 5, 2)]
        [TestCase(0, 6, 7)]
        public void Integration_GameFlow_TwoMoves_CheckSummary(int a, int b, int c)
        {
            int playerTurn = a; 
            bool startPhase1 = true;
            bool startPhase2 = true;
            int move1Index = b; 
            int move2Index = c; 

            _mechanic.TurnChoose(_players, _board, ref playerTurn, ref move1Index, ref startPhase1, 0, 1);

            _mechanic.TurnChoose(_players, _board, ref playerTurn, ref move2Index, ref startPhase2, 1, 0);

            bool isGameOver = _mechanic.WinDraw(_players, _board, ref _score);

            Assert.That(_board[b], Is.EqualTo('X'), "На позиції 0 має бути Х");
            Assert.That(_board[c], Is.EqualTo('O'), "На позиції 4 має бути О");
            Assert.That(playerTurn, Is.EqualTo(0), "Черга має знову стати 0");
            Assert.That(isGameOver, Is.False, "Гра ще не закінчилася");
            Assert.That(_score[0], Is.EqualTo(0), "Рахунок Гравця 1 має бути 0");
            Assert.That(_score[1], Is.EqualTo(0), "Рахунок Гравця 2 має бути 0");
        }

        [Test]
        public void WinDraw_Draw_ScoreRemainsTheSame_ReturnsTrue()
        {
            _board[0] = 'X';
            _board[1] = 'O';
            _board[2] = 'X';
            _board[3] = 'X';
            _board[4] = 'O';
            _board[5] = 'X';
            _board[6] = 'O';
            _board[7] = 'X';
            _board[8] = 'O';

            bool isGameOver = _mechanic.WinDraw(_players, _board, ref _score);

            Assert.That(isGameOver, Is.True, "Гра мала закінчитись");
            Assert.That(_score[0], Is.EqualTo(0), "Рахунок Гравця 1 не мав змінитись");
            Assert.That(_score[1], Is.EqualTo(0), "Рахунок Гравця 2 не мав змінитись");
        }
    }
}