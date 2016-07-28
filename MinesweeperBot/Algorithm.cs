using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    abstract class Algorithm
    {
        protected GameField msGame;
        protected bool hasFoundAMove;

        public void Execute()
        {
            FindFlags();
            FindSafeTiles();

        }
        
           
        protected Algorithm()
        {
            msGame = GameField.Instance;
            hasFoundAMove = false;
        }

        // Make sure it updates Mouse on each flag found
        abstract protected void FindFlags();
        abstract protected void FindSafeTiles();

        /*protected Tile[,] GetSurroundingTiles(int row, int col)
        {
            Tile[,] surroundingTiles = new Tile[3, 3];

            bool top = false, left = false, right = false, bottom = false;
            if(row == 0)
            {
                top = true;
            }

            if(row == msGame.Height-1)
            {
                bottom = true;
            }

            if(col == 0)
            {
                left = true;
            }

            if(col == msGame.Width-1)
            {
                right = true;
            }

            //Surroundee
            surroundingTiles[1, 1] = msGame.Get(row, col);
            
            //Top-left
            if(top || left) { surroundingTiles[0, 0] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[0, 0] = msGame.Get(row - 1, col - 1); }

            //Top
            if (top) { surroundingTiles[0, 1] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[0, 1] = msGame.Get(row - 1, col); }

            //Top-right
            if (top || right) { surroundingTiles[0, 2] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[0, 2] = msGame.Get(row - 1, col + 1); }

            //Left
            if (left) { surroundingTiles[1, 0] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[1, 0] = msGame.Get(row, col - 1); }

            //Right
            if (right) { surroundingTiles[1, 2] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[1, 2] = msGame.Get(row, col + 1); }

            //Bottom-left
            if (bottom || left) { surroundingTiles[2, 0] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[2, 0] = msGame.Get(row + 1, col - 1); }

            //Bottom
            if (bottom) { surroundingTiles[2, 1] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[2, 1] = msGame.Get(row + 1, col); }

            //Bottom-right
            if (bottom || right) { surroundingTiles[2, 2] = new Tile(TileType.Empty, 0, 0); }
            else { surroundingTiles[2, 2] = msGame.Get(row + 1, col + 1); }

            return surroundingTiles;
        }
        */

        protected Tile[] GetSurroundingTiles(int row, int col)
        {
            Tile[] surroundingTiles = new Tile[9];

            bool top = false, left = false, right = false, bottom = false;
            if (row == 0)
            {
                top = true;
            }

            if (row == msGame.Height - 1)
            {
                bottom = true;
            }

            if (col == 0)
            {
                left = true;
            }

            if (col == msGame.Width - 1)
            {
                right = true;
            }

            //Surroundee
            surroundingTiles[4] = msGame.Get(row, col);

            //Top-left
            if (top || left) { surroundingTiles[0] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[0] = msGame.Get(row - 1, col - 1); }

            //Top
            if (top) { surroundingTiles[1] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[1] = msGame.Get(row - 1, col); }

            //Top-right
            if (top || right) { surroundingTiles[2] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[2] = msGame.Get(row - 1, col + 1); }

            //Left
            if (left) { surroundingTiles[3] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[3] = msGame.Get(row, col - 1); }

            //Right
            if (right) { surroundingTiles[5] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[5] = msGame.Get(row, col + 1); }

            //Bottom-left
            if (bottom || left) { surroundingTiles[6] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[6] = msGame.Get(row + 1, col - 1); }

            //Bottom
            if (bottom) { surroundingTiles[7] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[7] = msGame.Get(row + 1, col); }

            //Bottom-right
            if (bottom || right) { surroundingTiles[8] = new Tile(TileType.Empty, 0, 0, 0, 0); }
            else { surroundingTiles[8] = msGame.Get(row + 1, col + 1); }

            return surroundingTiles;
        }

        protected enum Side
        {
            TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight, Invalid
        }

    }
}
