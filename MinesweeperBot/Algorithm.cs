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

        /* This method is used to calculate the
         * effective number of every numbered tile
         * that has not already been checked or flagged
         */
        protected void CalculateEffectiveNumber()
        {
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    Tile currentTile = msGame.Get(i, j);
                    if (currentTile.IsNumber && !currentTile.Checked && !currentTile.Flagged)
                    {
                        int flagCount = 0;
                        foreach (Tile tile in GetSurroundingTiles(i, j))
                        {
                            if (tile.Type == TileType.Flag) { flagCount++; }
                        }
                        currentTile.EffectiveNumber = (int)currentTile.Type - flagCount;
                        // System.Console.WriteLine("N is " + (int)currentTile.Type + " and EN is " + currentTile.EffectiveNumber);
                    }
                }
            }
        }

        /* 
        * This method counts the number of unclicked tiles surrounding the 
        * original tile being investigated.
        */
        protected int CountSurroundingUnclickedTiles(Tile[] surroundingTiles)
        {
            int count = 0;
            foreach (Tile tile in surroundingTiles)
            {
                if (tile.Type == TileType.Unclicked) { count++; }
            }
            return count;
        }

        protected enum Side
        {
            TopLeft, Top, TopRight, Left, Center, Right, BottomLeft, Bottom, BottomRight, Invalid
        }

    }
}
