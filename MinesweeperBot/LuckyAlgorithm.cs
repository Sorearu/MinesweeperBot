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
            // Iterate through game field
            // Find effective ones
            // If it has 2 unclicked tiles surrounding it, add it to pool of tiles
            // At end, randomly select one tile to click
        }
    }
}
