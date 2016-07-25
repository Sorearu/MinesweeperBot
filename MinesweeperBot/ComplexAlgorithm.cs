using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    // TO DO
    class ComplexAlgorithm : Algorithm
    {
        /* This FindFlags method will use the 1-2 pattern in order to find flags
         * 
         */
        protected override void FindFlags()
        {
            CalculateEffectiveNumber();
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    Tile currentTile = msGame.Get(i, j);
                    if(currentTile.EffectiveNumber == 2)
                    {
                        Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                        if(CountSurroundingUnclickedTiles(surroundingTiles) == 3)
                        {
                            // Checks if '2' is on the edge
                            Side edgeSide = DetermineEdgeSide(surroundingTiles, TileType.Unclicked);
                            if (edgeSide != Side.Invalid)
                            {
                                // Check if there are '1's beside it
                                Side oneSide = DetermineTileSide(surroundingTiles, 1, edgeSide);
                                if(oneSide != Side.Invalid)
                                {
                                    // Flag the mine to the opposite side of where the 1 is.
                                    Tile tile = surroundingTiles[(int)oneSide];
                                    Mouse.MoveToAndClick(tile.X, tile.Y, MouseButton.Right);
                                    currentTile.EffectiveNumber--;
                                    msGame.Update();

                                }
                            }
                        }     
                    }
                }
            }
        }

        /* This FindSafeTiles() method will use the 1-1 pattern in order to find
         * safe tiles
         */
        protected override void FindSafeTiles()
        {
            CalculateEffectiveNumber();
            msGame.State = State.Simple;
        }

        private int CountSurroundingUnclickedTiles(Tile[] surroundingTiles)
        {
            int count = 0;
            foreach (Tile tile in surroundingTiles)
            {
                if(tile.Type == TileType.Unclicked) { count++; }
            }
            return count;
        }

        private Side DetermineEdgeSide(Tile[] surroundingTiles, TileType type)
        {
            Side side = Side.Invalid;
            //Top
            if (surroundingTiles[0].Type == type && surroundingTiles[1].Type == type &&
                surroundingTiles[2].Type == type) { side = Side.Top; }

            //Bottom
            else if (surroundingTiles[6].Type == type && surroundingTiles[7].Type == type &&
                surroundingTiles[8].Type == type) { side = Side.Bottom; }

            //Left
            else if (surroundingTiles[0].Type == type && surroundingTiles[3].Type == type &&
                surroundingTiles[6].Type == type) { side = Side.Left; }

            //Right
            else if (surroundingTiles[2].Type == type && surroundingTiles[5].Type == type &&
                surroundingTiles[8].Type == type) { side = Side.Right; }
            return side;
        }

        private Side DetermineTileSide(Tile[] surroundingTiles, int effectiveNumber, Side edgeSide)
        {
            Side tileSide = Side.Invalid;

            //Top
            if(edgeSide == Side.Top)
            {
                //Left
                if(surroundingTiles[3].EffectiveNumber == effectiveNumber) { tileSide = Side.TopRight; }

                //Right
                else if(surroundingTiles[5].EffectiveNumber == effectiveNumber) { tileSide = Side.TopLeft; }
            }

            //Bottom
            else if (edgeSide == Side.Bottom)
            {
                //Left
                if (surroundingTiles[3].EffectiveNumber == effectiveNumber) { tileSide = Side.BottomRight; }

                //Right
                else if (surroundingTiles[5].EffectiveNumber == effectiveNumber) { tileSide = Side.BottomLeft; }
            }

            //Left
            else if (edgeSide == Side.Left)
            {
                //Top
                if (surroundingTiles[1].EffectiveNumber == effectiveNumber) { tileSide = Side.BottomLeft; }

                //Bottom
                else if (surroundingTiles[7].EffectiveNumber == effectiveNumber) { tileSide = Side.TopLeft; }
            }

            //Right
            else if (edgeSide == Side.Right)
            {
                //Top
                if (surroundingTiles[1].EffectiveNumber == effectiveNumber) { tileSide = Side.BottomRight; }

                //Bottom
                else if (surroundingTiles[7].EffectiveNumber == effectiveNumber) { tileSide = Side.TopRight; }
            }
            return tileSide;
        }

        private void CalculateEffectiveNumber()
        {
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    Tile currentTile = msGame.Get(i, j);
                    if(currentTile.IsNumber && !currentTile.Checked && !currentTile.Flagged)
                    {
                        int flagCount = 0;
                        foreach(Tile tile in GetSurroundingTiles(i, j))
                        {
                            if(tile.Type == TileType.Flag) { flagCount++; }
                        }
                        currentTile.EffectiveNumber = (int)currentTile.Type - flagCount;
                    }
                }
            }
        }


    }

    

}
