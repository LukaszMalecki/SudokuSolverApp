using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.SudokuLogic
{
    public class TileFill
    {
        public Tile FilledTile { get; private set; }
        [Range(Tile.MinNumberValue, Tile.MaxNumberValue)]
        public int ChosenNumber { get; private set; } = Tile.DefaultChosenNumber;
        public Difficulty Difficulty { get;  set; } 
        public void FillTile()
        {
            FilledTile.FillChosenNumber(ChosenNumber);
        }
    }

    public enum Difficulty
    {
        Easy, Medium, Hard, Expert
    }
}
