using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
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
            // Iterate through field
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    // Check that its effective number is 1
                    Tile currentTile = msGame.Get(i, j);
                    if (currentTile.EffectiveNumber == 1)
                    {
                        // Check that there are only 2 unclicked tiles
                        Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                        if (CountSurroundingUnclickedTiles(surroundingTiles) == 2)
                        {
                            // Check if unclicked are next to each other
                            Side[] sides = CheckIfUnclickedAreAdjacent(surroundingTiles);
                            if(sides[0] != Side.Invalid)
                            {
                                // Check if effective 1 is under that
                                Side oneSide = GetTileFromSides(sides);
                                Tile oneTile = surroundingTiles[(int)oneSide];
                                if(oneTile.EffectiveNumber == 1)
                                {
                                    // Click appropriate tile.
                                    Tile[] oneSurroundingTiles = GetSurroundingTiles(oneTile.Row, oneTile.Column);
                                    Tile tile = oneSurroundingTiles[(int)sides[1]];
                                    if(tile.Type == TileType.Unclicked)
                                    {
                                        Mouse.MoveToAndClick(tile.X, tile.Y, MouseButton.Left);
                                        msGame.Update();
                                    }
                                }
                            }                            
                        }
                    }
                }
            }
            msGame.State = State.Simple;
        }

        /* Helper method for both FindFlags() and FindSafeTiles()
         * This method counts the number of unclicked tiles surrounding the 
         * original tile being investigated.
         */
        private int CountSurroundingUnclickedTiles(Tile[] surroundingTiles)
        {
            int count = 0;
            foreach (Tile tile in surroundingTiles)
            {
                if(tile.Type == TileType.Unclicked) { count++; }
            }
            return count;
        }

        /* Helper method for FindFlags()
         * This method determines, if there are three unclicked tiles in a row/column, what
         * side it is on.
         */
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

        /* Helper method for FindFlags().
         * This method finds the tile to be flagged given the side that the unclicked tiles
         * are on relative to the original tile being investigated.
         */
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

        /* Helper method for FindSafeTiles()
         * This method determines if the two unclicked tiles are next to each other.
         * If so, it puts their side in the array and returns it.
         */
        private Side[] CheckIfUnclickedAreAdjacent(Tile[] surroundingTiles)
        {
            Side[] sides = { Side.Invalid, Side.Invalid };

            // top
            if(surroundingTiles[(int)Side.Top].Type == TileType.Unclicked)
            {
                if(surroundingTiles[(int)Side.TopLeft].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Top;
                    sides[1] = Side.TopLeft;
                }

                else if (surroundingTiles[(int)Side.TopRight].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Top;
                    sides[1] = Side.TopRight;
                }
            }

            // bottom
            if (surroundingTiles[(int)Side.Bottom].Type == TileType.Unclicked)
            {
                if (surroundingTiles[(int)Side.BottomLeft].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Bottom;
                    sides[1] = Side.BottomLeft;
                }

                else if (surroundingTiles[(int)Side.BottomRight].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Bottom;
                    sides[1] = Side.BottomRight;
                }
            }

            // left
            if (surroundingTiles[(int)Side.Left].Type == TileType.Unclicked)
            {
                if (surroundingTiles[(int)Side.TopLeft].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Left;
                    sides[1] = Side.TopLeft;
                }

                else if (surroundingTiles[(int)Side.BottomLeft].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Left;
                    sides[1] = Side.BottomLeft;
                }
            }

            // right
            if (surroundingTiles[(int)Side.Right].Type == TileType.Unclicked)
            {
                if (surroundingTiles[(int)Side.TopRight].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Right;
                    sides[1] = Side.TopRight;
                }

                else if (surroundingTiles[(int)Side.BottomRight].Type == TileType.Unclicked)
                {
                    sides[0] = Side.Right;
                    sides[1] = Side.BottomRight;
                }
            }

            return sides;
        }

        /* Helper method for FindSafeTiles().
         * This method determines the side of a tile given the sides of
         * the two unclicked tiles.
         */
        private Side GetTileFromSides(Side[] sides)
        {
            Side tileSide;

            if(sides[0] == Side.Left && sides[1] == Side.TopLeft ||
                sides[0] == Side.Right && sides[1] == Side.TopRight)
            {
                tileSide = Side.Top;
            }

            else if (sides[0] == Side.Left && sides[1] == Side.BottomLeft ||
                sides[0] == Side.Right && sides[1] == Side.BottomRight)
            {
                tileSide = Side.Bottom;
            }

            else if (sides[0] == Side.Top && sides[1] == Side.TopLeft ||
                sides[0] == Side.Bottom && sides[1] == Side.BottomLeft)
            {
                tileSide = Side.Left;
            }

            else
            {
                tileSide = Side.Right;
            }

            return tileSide;
        }

        /* This method is used to calculate the
         * effective number of every numbered tile
         * that has not already been checked or flagged
         */
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
                       // System.Console.WriteLine("N is " + (int)currentTile.Type + " and EN is " + currentTile.EffectiveNumber);
                    }
                }
            }
        }


    }

    

}
