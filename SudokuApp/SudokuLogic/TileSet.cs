using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.SudokuLogic
{
    public class TileSet
    {
        public Area Area { get; private set; }
        public GameBoard Board { get; private set; }
        public List<TileSet> OverlappingSets { get; private set; } = new List<TileSet>();
        public TileSet(GameBoard board, Area area) 
        {
            Board = board;
            Area = area;
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
    }
}
