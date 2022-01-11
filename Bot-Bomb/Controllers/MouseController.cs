using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Bomb.Controllers
{
    public class MouseController
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static void MoveCursor(Rectangle location)
        {
            Cursor.Position = new Point(location.X + location.Width / 2, location.Y + location.Height / 2);
            DoMouseClick();
        }
        public static void DoMouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public static Boolean FindLocation(Bitmap SecondScreen, double tolerance)
        {
            Boolean resultado = true;

            using (Bitmap ThreedScreen = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\ScreenShot.bmp"))
            {
                Rectangle location2 = new Rectangle();
                Bitmap tela = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\ScreenShot.bmp");
                Rectangle location1 = CaptureController.searchBitmap(SecondScreen, tela, tolerance);
                int x = 0, y = 0;
                Console.WriteLine("X: " + location1.X + "\nY: " + location1.Y + "\nWidth: " + location1.Width + "\nHeight: " + location1.Height);
                for (x = 0; x < ThreedScreen.Width; x++)
                {
                    for (y = 0; y < ThreedScreen.Height; y++)
                    {
                        if (x >= location1.X && y >= location1.Y)
                        {
                            if (y <= location1.Y + location1.Height)
                            {
                                if (x <= location1.X + location1.Width)
                                {
                                    if (x == Convert.ToInt32((location1.X + (location1.Width / 2))) && y == Convert.ToInt32((location1.Y + (location1.Height / 2))))
                                    {
                                        location2.X = x;
                                        location2.Y = y;
                                    }
                                    Color pixelColor = ThreedScreen.GetPixel(x, y);
                                    Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, 0);
                                    ThreedScreen.SetPixel(x, y, newColor);
                                }
                            }
                        }
                    }
                }
                ThreedScreen.Save(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\Finish.bmp", ImageFormat.Bmp);
                if (location1.X == 0 && location1.Y == 0)
                {
                    resultado = false;
                }
                else
                {
                    MouseController.MoveCursor(location1);
                }
                Console.WriteLine("Pronto");
            }
            return resultado;
        }

    }
}
