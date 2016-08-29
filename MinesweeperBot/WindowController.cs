using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperBot
{
    class WindowController
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // Uses windows32 api in order to manipulate the ms window, as well as details about its resolution
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        const int SW_RESTORE = 9;

        private static Process[] processes;
        private static IntPtr handle;
        private static RECT rect;
        private static int windowWidth, windowHeight;

        static WindowController()
        {
            processes = Process.GetProcessesByName("Winmine__XP");
            handle = processes[0].MainWindowHandle;

            GetWindowRect(handle, out rect);
            windowWidth = rect.Right - rect.Left;
            windowHeight = rect.Bottom - rect.Top;

           // System.Console.WriteLine("The width is " + windowWidth + " and the height is " + windowHeight);

        }

        // Moves the game window to the front and top-left of the screen.
        public static void SetUpGameWindow()
        {
            SetForegroundWindow(handle);
            MoveWindow(handle, 0, 0, windowWidth, windowHeight, true);
            Thread.Sleep(1000);
        }

        public static int WindowWidth
        {
            get { return windowWidth; }
        }

        public static int WindowHeight
        {
            get { return windowHeight; }
        }

        public static Bitmap TakeScreenshot()
        {
            Bitmap bitmapSS = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);

            Graphics graphicsSS = Graphics.FromImage(bitmapSS);

            graphicsSS.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                            0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            
            return bitmapSS;


        }

    }
}
