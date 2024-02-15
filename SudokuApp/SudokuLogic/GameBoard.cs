using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.SudokuLogic
{
    public class GameBoard
    {
        protected const int DefaultBoardRowCount = 9;
        protected const int DefaultBoardColCount = 9;
        protected const int DefaultSquareSetLen = 3;
        protected const int DefaultLineSetLen = 9;
        public Area Area { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public List<TileSet> TileSets { get; private set; }
        public List<TileSet> SquareTileSets { get; private set; }
        public List<TileSet> HorizontalTileSets { get; private set; }
        public List<TileSet> VerticalTileSets { get; private set; }

        public GameBoard() 
        {
            Initialize();
        }
        private void Initialize()
        {
            Area = new Area(0, 0, DefaultBoardRowCount, DefaultBoardColCount);
            Tiles = new Tile[Area.RowCount, Area.ColCount];

            for (int r = Area.RowStart; r < Area.RowStart + Area.RowCount; r++)
            {
                for (int c = Area.ColStart; c < Area.ColStart + Area.ColCount; c++)
                {
                    Tiles[r, c] = new Tile(this, r, c);
                }
            }
            TileSets = new List<TileSet>();
            SquareTileSets = new List<TileSet>();
            HorizontalTileSets = new List<TileSet>();
            VerticalTileSets = new List<TileSet>();

            TileSet tempSet = null;

            for (int r = Area.RowStart; r < Area.RowStart + Area.RowCount; r+=DefaultSquareSetLen)
            {
                for (int c = Area.ColStart; c < Area.ColStart + Area.ColCount; c+=DefaultSquareSetLen)
                {
                    tempSet = new TileSet(this, new Area(r, c, DefaultSquareSetLen, DefaultSquareSetLen));
                    TileSets.Add(tempSet);
                    SquareTileSets.Add(tempSet);
                }
            }

            for (int i = Area.RowStart; i < Area.RowStart + Area.RowCount; i++)
            {
                tempSet = new TileSet(this, new Area( rowStart: i, Area.ColStart, rowCount: 1, colCount: DefaultLineSetLen));
                TileSets.Add(tempSet);
                HorizontalTileSets.Add(tempSet);
            }

            for (int i = Area.ColStart; i < Area.ColStart + Area.ColCount; i++)
            {
                tempSet = new TileSet(this, new Area(rowStart: Area.RowStart, i, rowCount: DefaultLineSetLen, colCount: 1));
                TileSets.Add(tempSet);
                VerticalTileSets.Add(tempSet);
            }
        }
        public Tile GetTile(int row, int col) 
        {
            return Tiles[row, col];
        }
        public Tile GetTile((int row, int col) coord)
        {
            return GetTile(coord.row, coord.col);
        }

        private void FillBoard()
        {

        }
    }
}
