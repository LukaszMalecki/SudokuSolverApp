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
        public const int DefaultCorrectNumber = 0;
        public const int NumberArrayLength = 10;
        public const int MinPossibleNumberValue = 0;
        public const int MinActualNumberValue = 1;
        public const int MaxNumberValue = 9;

        public int Row { get; private set; }
        public int Col { get; private set; }
        public GameBoard Board { get; private set; }
        public List<TileSet> TileSets { get; private set; } = new List<TileSet>();
        [Range(MinPossibleNumberValue, MaxNumberValue)]
        public int CorrectNumber { get; private set; } = DefaultCorrectNumber;
        [Range(MinPossibleNumberValue, MaxNumberValue)]
        public int ChosenNumber { get; private set; } = DefaultChosenNumber;

        //index corresponds to the number, as such 0 isn't used
        public bool[] NotesNumbers { get; private set; } = new bool[NumberArrayLength];
        public bool CanBeChanged { get; private set; } = true;
        public TileStatus TileStatus { get; private set; } = TileStatus.Unfilled;

        public Tile(GameBoard board, int row, int col, int? correctNumber = null)
        {
            this.Board = board;
            Row = row;
            Col = col;
            if(correctNumber != null)
            {
                SetCorrectNumber(correctNumber.Value);
            }
        }
        public (int row, int col) GetPoint()
        {
            return (Row, Col);
        }

        public void SetCorrectNumber(int number, bool updateTileSet = true)
        {
            CorrectNumber = number;
            if(updateTileSet)
            {
                foreach (TileSet tileSet in TileSets)
                {
                    tileSet.AddCorrectNumber(number);
                }
            }
        }
        public void RevealCorrectNumber()
        {
            ChosenNumber = CorrectNumber;
            CanBeChanged = false;
            UpdateStatus();
        }
        private void SetChosenNumber(int number)
        {
            ChosenNumber = number;
            UpdateStatus();
            ClearNotesNumbers();
        }
        private void SetNotesNumber(int number)
        {
            NotesNumbers[number] = !NotesNumbers[number];
        }
        private void ClearNotesNumbers()
        {
            for(int i = 0; i < NotesNumbers.Length; i++)
            {
                NotesNumbers[i] = false;
            }
        }
        public void EraseChosenNumber()
        {
            if(!CanBeChanged) 
                return;
            SetChosenNumber(DefaultChosenNumber);
        }
        public void FillChosenNumber(int number)
        {
            if(!CanBeChanged)
            {
                return;
            }
            if(number == ChosenNumber)
            {
                EraseChosenNumber();
                return;
            }
            SetChosenNumber(number);
        }
        public void AddNote(int number)
        {
            if (TileStatus != TileStatus.Unfilled)
                return;
            SetNotesNumber(number);
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

        public List<int> GetPossibleCorrectNumbers()
        {
            LinkedList<int> possibleNumbers = new LinkedList<int>();
            for( int i = MinActualNumberValue; i <= MaxNumberValue; i++)
                possibleNumbers.AddLast(i);

            foreach( TileSet tileSet in TileSets)
            {
                if (tileSet.RemainingCorrectNumbers == TileSet.MaxNumberCount)
                    continue;
                var node = possibleNumbers.First;
                while( node != null)
                {
                    var nextNode = node.Next;
                    if(tileSet.ContainsCorrectNumber(node.Value))
                    {
                        possibleNumbers.Remove(node);
                    }
                    node = nextNode;
                }
            }
            return possibleNumbers.ToList();
        }

        public bool SetRandomCorrectNumber(Random random)
        {
            if (CorrectNumber != 0)
                return true;
            var possibleNumbers = GetPossibleCorrectNumbers();
            if(possibleNumbers.Count == 0)
                return false;
            int numIndex = random.Next(possibleNumbers.Count);
            SetCorrectNumber(possibleNumbers[numIndex]);
            return true;
        }
        public bool IsTileCorrectlyInitialized()
        {
            return CorrectNumber != DefaultCorrectNumber;
        }
    }

    public enum TileStatus
    {
        Unfilled, CorrectlyFilled, IncorrectlyFilled, StarterTile
    }
}
