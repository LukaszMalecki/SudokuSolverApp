using SudokuApp.SudokuLogic;
using System;

namespace SudokuApp
{
    internal class Program
    {
        //From tests we can see, that generating proper gameboard could also use make use of Sudoku solving algorithms
        //Therefore, for now boards are to be supplied by the user, proper methods and formats will be introduced in further updates
        private static void TestBoardGeneration()
        {
            GameBoard board = new GameBoard();
            board.TestPrintCorrectNumberBoard(true);
        }
        static void Main(string[] args)
        {
            TestBoardGeneration();
        }

    }
}
