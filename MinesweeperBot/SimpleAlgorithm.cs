using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    class SimpleAlgorithm : Algorithm
    {
        protected override void FindFlags()
        {
            // Iterate through all tiles
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    Tile currentTile = msGame.Get(i, j);
                    if(currentTile.IsNumber && !currentTile.Flagged)
                    {
                        // Checks if there are a valid number of mines surrounding the tile
                        int number = (int)currentTile.Type;
                        Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                        int[] flagPositions = GetPositionsOfSurroundingFlags(surroundingTiles, number);

                        if (flagPositions[0] != -1)
                        {
                            if(flagPositions[0] == 2)
                            {
                                currentTile.Flagged = true;
                                currentTile.EffectiveNumber = 0;
                            }

                            else
                            {
                                for (int m = 0; m < flagPositions.Length; m++)
                                {
                                    // Flags all confirmed mines
                                    if (flagPositions[m] == 1)
                                    {
                                        Tile tempTile = surroundingTiles[m];
                                        Mouse.MoveToAndClick(tempTile.X, tempTile.Y, MouseButton.Right);
                                    }
                                }
                                msGame.Update();
                                currentTile.Flagged = true;
                                currentTile.EffectiveNumber = 0;
                                hasFoundAMove = true;
                            }                            
                        }
                    }
                }
            }
        }

        protected override void FindSafeTiles()
        {
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    Tile currentTile = msGame.Get(i, j);
                    if(currentTile.IsNumber && !currentTile.Checked)
                    {
                        int number = (int)currentTile.Type;
                        Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                        if (SurroundingFlagsEqualToNumber(surroundingTiles, number))
                        {
                            foreach (Tile tile in surroundingTiles)
                            {
                                if (tile.Type == TileType.Unclicked)
                                {
                                    Mouse.MoveToAndClick(tile.X, tile.Y, MouseButton.Left);
                                    msGame.Update();
                                }
                            }
                            currentTile.Checked = true;
                            currentTile.EffectiveNumber = 0;
                            hasFoundAMove = true;
                        }                       
                    }
                    
                }
            }
           if(!hasFoundAMove) { msGame.State = State.Complex; }

        }

        /* Used to create a 2D array determining whether that position in the surroundingTiles array
         * is a mine or not.
         * If position [0,0] in output is -1, then it is invalid. If it is 2, then it has already been
         * flagged by another surrounding tile.
         */
        private int[] GetPositionsOfSurroundingFlags(Tile[] surroundingTiles, int numberTile)
        {
            int[] flagPositions = new int[9];
            int flagCount = 0;
            int alreadyFlaggedCount = 0;

            for (int i = 0; i < surroundingTiles.Length; i++)
            {
                Tile tile = surroundingTiles[i];
                if (tile.Type == TileType.Unclicked || tile.Type == TileType.Flag)
                {
                    if (flagCount >= numberTile)
                    {
                        // Indicates there are more unclicked tiles than the number indicated
                        // Therefore, no valid flags
                        flagPositions[0] = -1;
                        flagCount = 0;
                    }

                    // Only change to 1 if it is unclicked and not already flagged.
                    if (tile.Type == TileType.Unclicked) { flagPositions[i] = 1; }
                    if (tile.Type == TileType.Flag) { alreadyFlaggedCount++; }
                    flagCount++;
                }
                
            }

            // Prevents code being unnecessarily executed
            if (alreadyFlaggedCount == numberTile) { flagPositions[0] = 2; }

            
            
            


            return flagPositions;
        }

        /* Used to determine if there is a number of flags surrounding a tile
         * that is equal to the number on that tile.
         * This means that all the unclicked tiles surrounding it are safe to click
         */
        private bool SurroundingFlagsEqualToNumber(Tile[] surroundingTiles, int number)
        {
            int count = 0;
            foreach (Tile tile in surroundingTiles)
            {
                if (tile.Type == TileType.Flag) { count++; }
            }

            if (count == number) { return true; }
            else { return false; }
        }
    }
}