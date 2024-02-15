using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.SudokuLogic
{
    public class TileSet
    {
        public const int MaxNumberCount = 9;
        public Area Area { get; private set; }
        public GameBoard Board { get; private set; }
        public List<TileSet> OverlappingSets { get; private set; } = new List<TileSet>();
        public List<Tile> Tiles { get; private set; }
        //index corresponds to the number, if array[0] == false then all others == true, else at least one of others == false
        public bool[] CorrectNumbers { get; private set; } = new bool[Tile.NumberArrayLength];
        public int RemainingCorrectNumbers { get; private set; } = MaxNumberCount;
        public bool[] ChosenNumbers { get; private set; } = new bool[Tile.NumberArrayLength];
        public int RemainingChosenNumbers { get; private set; } = MaxNumberCount;
        public TileSet(GameBoard board, Area area) 
        {
            Board = board;
            Area = area;
            InitializeTilesList();
        }
        public bool IsSquare()
        {
            return Area.IsSquare();
        }
        public void UpdateCorrectNumbers()
        {
            ResetCorrectNumbers();
            RemainingChosenNumbers = MaxNumberCount;
            CorrectNumbers[0] = true;
            foreach( Tile tile in Tiles)
            {
                if(!CorrectNumbers[tile.CorrectNumber])
                {
                    RemainingChosenNumbers--;
                    CorrectNumbers[tile.CorrectNumber] = true;
                }
            }
            if(RemainingChosenNumbers == 0)
            {
                CorrectNumbers[0] = false;
            }
        }
        private void ResetCorrectNumbers()
        {
            for( int i = 0; i < CorrectNumbers.Length; i++)
            {
                CorrectNumbers[i] = false;
            }
        }
        private void InitializeTilesList()
        {
            Tiles = new List<Tile>();
            foreach( (int r, int c) in Area.GetPoints())
            {
                Tiles.Add(Board.GetTile(r, c));
            }
            AttachToTiles();
        }
        private void AttachToTiles()
        {
            foreach( Tile tile in Tiles)
            {
                tile.AddTileSet(this);
            }
        }
        public bool ContainsCorrectNumber(int number)
        {
            return CorrectNumbers[number];
        }
    }
    public class Area
    {
        public int RowStart { get; set; }
        public int ColStart { get; set; }
        public int RowCount { get; set; }
        public int ColCount { get; set; }
        public Area(int rowStart = 0, int colStart = 0, int rowCount = 3, int colCount = 3)
        {
            RowStart = rowStart;
            ColStart = colStart;
            RowCount = rowCount;
            ColCount = colCount;
        }

        public bool IsSquare()
        {
            return RowCount == ColCount;
        }
        public List<(int row, int col)> GetPoints()
        {
            List<(int row, int col)> retList = new List<(int row, int col)>();
            for( int r = RowStart; r < RowStart + RowCount; r++ ) 
            {
                for( int  c = ColStart; c < ColStart + ColCount; c++ ) 
                {
                    retList.Add((r, c));
                }
            }
            return retList;
        }
        public bool ContainsPoint( int row, int col)
        {
            if( row < RowStart || row >= RowStart + RowCount)
                return false;
            if (col < ColStart || col >= ColStart + ColCount)
                return false;
            return true;
        }
    }
}
