using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MinesweeperBot
{
    class Mouse
    {
        // Uses win32 api in order to control mouse clicks
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int LEFTDOWN = 0x02;
        public const int LEFTUP = 0x04;
        public const int RIGHTDOWN = 0x08;
        public const int RIGHTUP = 0x10;



        // Probably remove the cursor movement, as it is unnecessary
        public static void MoveToAndClick(int x, int y, MouseButton button)
        {
            Cursor.Position = new Point(x, y);
            if(button == MouseButton.Left)
            {
                mouse_event(LEFTDOWN, x, y, 0, 0);
                mouse_event(LEFTUP, x, y, 0, 0);
            }
            else if(button == MouseButton.Right)
            {
                mouse_event(RIGHTDOWN, x, y, 0, 0);
                mouse_event(RIGHTUP, x, y, 0, 0);
            }
            else if(button == MouseButton.Both)
            {
                mouse_event(RIGHTDOWN, x, y, 0, 0);
                mouse_event(LEFTDOWN, x, y, 0, 0);
                mouse_event(RIGHTUP, x, y, 0, 0);
                mouse_event(LEFTUP, x, y, 0, 0);
                
            }
            Cursor.Position = new Point(0, 0);
            
        }

        



    }

    enum MouseButton
    {
        Left, Right, Both, None
    }

    
}
