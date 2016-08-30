using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    // TO DO
    class LuckyAlgorithm : Algorithm
    {
        protected override void FindFlags()
        {
        }

        protected override void FindSafeTiles()
        {
            CalculateEffectiveNumber();
            int tileCount = 0;
            Tile[] tilePool = new Tile[msGame.Height * msGame.Width];
        
            // offset refers to how many surrounding unclicked tiles we want to check for in relation
            // to the effective tile number
            for (int offset = 1; offset < 7; offset++)
            {
                // Iterate through all possible effectiveTileNumbers (1-7)
                for (int effectiveTileNumber = 1; effectiveTileNumber <= 7; effectiveTileNumber++)
                {
                    Boolean foundTile = false;

                    // Iterate over gamefield
                    if (effectiveTileNumber + offset <= 8)
                    {
                        for (int i = 0; i < msGame.Height; i++)
                        {
                            for (int j = 0; j < msGame.Width; j++)
                            {
                                // Find effective numbered tile
                                Tile currentTile = msGame.Get(i, j);
                                if (currentTile.EffectiveNumber == effectiveTileNumber)
                                {
                                    // Check if correct amount of surrounding unclicked tiles
                                    Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                                    if (CountSurroundingUnclickedTiles(surroundingTiles) == effectiveTileNumber + offset)
                                    {
                                        foundTile = true;
                                        // If so, add those tiles to pool
                                        foreach (Tile tile in surroundingTiles)
                                        {
                                            if (tile.Type == TileType.Unclicked) { tilePool[tileCount++] = tile; }

                                        }
                                    }
                                }
                            }
                        }


                        // After loop, click random tile from pool (if found tile to click)
                        if (foundTile)
                        {
                            Random rng = new Random();
                            Tile luckyTile = tilePool[rng.Next(0, tileCount)];
                            Mouse.MoveToAndClick(luckyTile.X, luckyTile.Y, MouseButton.Left);
                            msGame.Update();
                            msGame.State = State.Simple;
                            return;
                        }
                    }                  
                }                  
            }          
        }
    }
}
