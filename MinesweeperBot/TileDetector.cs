using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperBot
{
    class TileDetector
      
    {
        private static Bitmap tileColours, smileyFace;

        static TileDetector()
        {
            tileColours = Properties.Resources.tileColours;
            smileyFace = Properties.Resources.smileyFace;

        }

        /*
         * This method determines the Tile Type by comparing 4 specified pixels of the tile with
         * a premade set of pixels in the tileColours.bmp file
         */
        public static TileType DetermineTileType(Color c1, Color c2, Color c3, Color c4)
        {
            TileType type = TileType.Unclicked;

            foreach (TileType typeTemp in Enum.GetValues(typeof(TileType)))
            {
                Color tc1 = tileColours.GetPixel((int)typeTemp, 0);
                Color tc2 = tileColours.GetPixel((int)typeTemp, 1);
                Color tc3 = tileColours.GetPixel((int)typeTemp, 2);
                Color tc4 = tileColours.GetPixel((int)typeTemp, 3);

                if(tc1.Equals(c1) && tc2.Equals(c2) && tc3.Equals(c3) && tc4.Equals(c4))
                {
                    type = typeTemp;
                    break;
                }
            }

            return type;
        }

        public static Health DetermineHealth(Color smileyColour1, Color smileyColour2)
        {
            foreach (Health health in Enum.GetValues(typeof(Health)))
            {
                Color c1 = smileyFace.GetPixel((int)health, 0);
                Color c2 = smileyFace.GetPixel((int)health, 1);

                if (c1.Equals(smileyColour1) && c2.Equals(smileyColour2))
                {
                    return health;
                }
            }

            return Health.Alive;

        }

    }
}
