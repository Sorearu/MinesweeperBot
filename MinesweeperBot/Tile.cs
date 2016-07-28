using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    class Tile
    {
        private TileType type;
        private int x, y, row, column, effectiveNumber;
        private bool isChecked, isFlagged, isNumber;
        
        public Tile(TileType type, int x, int y, int row, int column)
        {
            this.type = type;
            this.x = x;
            this.y = y;
            this.row = row;
            this.column = column;
            isChecked = false;
            isFlagged = false;

            if((int) type >= 1 && (int) type <= 9)
            {
                isNumber = true;
                effectiveNumber = (int)type;
            }
            else
            {
                isNumber = false;
                effectiveNumber = 10;
            }
        }
        
        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public TileType Type
        {
            get { return type; }
        }

        public bool Checked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public bool Flagged
        {
            get { return isFlagged; }
            set { isFlagged = value; }
        }

        public bool IsNumber
        {
            get { return isNumber; }
        }

        public int EffectiveNumber
        {
            get { return effectiveNumber; }
            set { effectiveNumber = value; }
        }

        public int Row
        {
            get { return row; }
        }

        public int Column
        {
            get { return column; }
        }
    }
    public enum TileType
    {
        Unclicked = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5,
        Six = 6, Seven = 7, Eight = 8, Nine = 9, Mine = 10, Flag = 11, Question = 12, Empty = 13, MineRed = 14
    }
}
