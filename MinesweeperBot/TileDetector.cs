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
        private static Bitmap tileColours;

        static TileDetector()
        {
            tileColours = Properties.Resources.tileColours;

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

    }
}
