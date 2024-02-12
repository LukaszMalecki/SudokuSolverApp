using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.SudokuLogic
{
    public class Tile
    {
        public const int DefaultChosenNumber = 0;
        public const int NotesNumberLength = 10;
        public const int MinNumberValue = 0;
        public const int MaxNumberValue = 9;

        public int X { get; private set; }
        public int Y { get; private set; }
        public GameBoard board { get; private set; }
        public List<TileSet> TileSets { get; private set; } = new List<TileSet>();
        [Range(MinNumberValue, MaxNumberValue)]
        public int CorrectNumber { get; private set; }
        [Range(MinNumberValue, MaxNumberValue)]
        public int ChosenNumber { get; private set; } = DefaultChosenNumber;

        //index corresponds to the number as such 0 isn't used
        public bool[] NotesNumbers { get; private set; } = new bool[NotesNumberLength];
        public bool CanBeChanged { get; private set; } = true;
        public TileStatus TileStatus { get; private set; } = TileStatus.Unfilled;

        public Tile(GameBoard board, int x, int y)
        {
            this.board = board;
            X = x;
            Y = y;
        }

        public void SetCorrectNumber(int number)
        {
            CorrectNumber = number;           
        }
        public void RevealCorrectNumber()
        {
            ChosenNumber = CorrectNumber;
            CanBeChanged = false;
            UpdateStatus();
        }
        public void FillChosenNumber(int number)
        {
            if(CanBeChanged)
            {
                ChosenNumber = number;
                UpdateStatus();
            }           
        }
        public void AddTileSet(TileSet tileset)
        {
            TileSets.Add(tileset);
        }
        private void UpdateStatus()
        {
            if( !CanBeChanged)
            {
                TileStatus = TileStatus.StarterTile;
                return;
            }

            if(ChosenNumber == DefaultChosenNumber)
            {
                TileStatus = TileStatus.Unfilled;
                return;
            }
            if(ChosenNumber != CorrectNumber)
            {
                TileStatus = TileStatus.IncorrectlyFilled;
                return;
            }
            TileStatus = TileStatus.CorrectlyFilled;
            return;
        }
    }

    public enum TileStatus
    {
        Unfilled, CorrectlyFilled, IncorrectlyFilled, StarterTile
    }
}
