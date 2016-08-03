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

            // Iterate over gamefield
            for (int i = 0; i < msGame.Height; i++)
            {
                for (int j = 0; j < msGame.Width; j++)
                {
                    // Find effective ones
                    Tile currentTile = msGame.Get(i, j);
                    if (currentTile.EffectiveNumber == 1)
                    {
                        // See if there are only 2 unclicked tiles
                        Tile[] surroundingTiles = GetSurroundingTiles(i, j);
                        if (CountSurroundingUnclickedTiles(surroundingTiles) == 2)
                        {
                            // If so, add those tiles to pool
                            foreach (Tile tile in surroundingTiles)
                            {
                                if (tile.Type == TileType.Unclicked) { tilePool[tileCount++] = tile; }

                            }                           
                        }
                    }
                }
            }

            // After loop, click random tile from pool

            if (tileCount > 0)
            {
                Random rng = new Random();
                Tile luckyTile = tilePool[rng.Next(0, tileCount)];
                Mouse.MoveToAndClick(luckyTile.X, luckyTile.Y, MouseButton.Left);
                msGame.Update();
            }

            msGame.State = State.Simple;
        }
    }
}
