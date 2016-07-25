using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    class InitialAlgorithm : Algorithm
    {
        public InitialAlgorithm() : base()
        {
        }
        protected override void FindFlags()
        {

        }

        protected override void FindSafeTiles()
        {
            Tile tile;
            int number = CountNumbers();

            // Clicks on different corners of the gamefield until we get a good opening.
            if (number == 0) { tile = msGame.Get(0, 0); }
            else if (number == 1) { tile = msGame.Get(0, msGame.Width - 1); }
            else if (number == 2) { tile = msGame.Get(msGame.Height - 1, 0); }
            else if (number == 3) { tile = msGame.Get(msGame.Height - 1, msGame.Width - 1); }
            else
            {
                tile = new Tile(TileType.Empty, 0, 0);
                msGame.State = State.Simple;
            }

            Mouse.MoveToAndClick(tile.X, tile.Y, MouseButton.Left);
            msGame.Update();

        }

        /* This method is used to determine the amount of 'numbered' tiles
         * on the field. This number is then used to determine if it is a
         * 'bad start'. 
         */
        private int CountNumbers()
        {
            int count = 0;

            for(int i = 0; i < msGame.Height; i++)
            {
                for(int j = 0; j < msGame.Width; j++)
                {
                    if (msGame.Get(i, j).IsNumber) { count++; }
                }
            }

            return count;
        }
    }
}
