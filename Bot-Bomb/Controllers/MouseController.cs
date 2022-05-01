using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Bomb.Controllers
{
    public class MouseController
    {
        public static List<Rectangle> First = new List<Rectangle>();
        public static List<Rectangle> Second = new List<Rectangle>();
        public static Boolean aux2 = false;
        public static int aux = 0;

        public static string pathOrigem = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

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
            Delay(1).Wait();
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public static Boolean FindLocation(string operador, double tolerance)
        {
            Boolean resultado = true;

            using (Bitmap ThreedScreen = new Bitmap(pathOrigem + @"\ScreenSave\ScreenShot.bmp"))
            {
                Rectangle location2 = new Rectangle();
                Rectangle location1 = CaptureController.searchBitmap(operador, tolerance);
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
                ThreedScreen.Save(pathOrigem + @"\ScreenSave\Finish.bmp", ImageFormat.Bmp);
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

        public static Boolean FindLocationList(string operador, double tolerance)
        {
            Boolean resultado = true;

            using (Bitmap ThreedScreen = new Bitmap(pathOrigem + @"\ScreenSave\ScreenShot.bmp"))
            {
                Rectangle location2 = new Rectangle();
                Rectangle location1 = CaptureController.searchBitmap(operador, tolerance);
                List<Rectangle> location3 = CaptureController.searchBitmapList(operador, tolerance);
                int x = 0, y = 0;
                Console.WriteLine("X: " + location1.X + "\nY: " + location1.Y + "\nWidth: " + location1.Width + "\nHeight: " + location1.Height);
                for (x = 0; x < ThreedScreen.Width; x++)
                {
                    for (y = 0; y < ThreedScreen.Height; y++)
                    {
                        for (int i = 0; i < location3.Count(); i++)
                        {
                            if (x >= location3[i].X && y >= location3[i].Y)
                            {
                                if (y <= location3[i].Y + location3[i].Height)
                                {
                                    if (x <= location3[i].X + location3[i].Width)
                                    {
                                        if (x == Convert.ToInt32((location3[i].X + (location3[i].Width / 2))) && y == Convert.ToInt32((location3[i].Y + (location3[i].Height / 2))))
                                        {
                                            location2.X = location3[i].X;
                                            location2.Y = location3[i].Y;
                                            location2.Width = location3[i].Width;
                                            location2.Height = location3[i].Height;
                                            if (aux == 0)
                                            {
                                                First.Add(location2);
                                            }
                                            if (aux == 1)
                                            {
                                                Second.Add(location2);
                                            }

                                        }
                                        Color pixelColor = ThreedScreen.GetPixel(x, y);
                                        Color newColor = Color.FromArgb(pixelColor.R, pixelColor.G, 0);
                                        ThreedScreen.SetPixel(x, y, newColor);
                                    }
                                }
                            }
                        }

                    }
                }
                ThreedScreen.Save(pathOrigem + @"\ScreenSave\Finish.bmp", ImageFormat.Bmp);
                if (location1.X == 0 && location1.Y == 0)
                {
                    resultado = false;
                }
                else
                {
                    Console.WriteLine("Contador: "+ location3.Count());
                    //MouseController.MoveCursor(location1);
                }
                Console.WriteLine("Pronto");
            }

            if (aux == 1)
            {

                Console.Clear();
                List<Rectangle> result = AlreadyToWork();
                Console.WriteLine("QUANTIDADE: " + result.Count());
                for (int i = 0; i < result.Count(); i++)
                {
                    Console.WriteLine("X: " + result[i].X + "\nY: " + result[i].Y + "\nWidth: " + result[i].Width + "\nHeight: " + result[i].Height);
                    MouseController.MoveCursor(result[i]);
                    Delay(1).Wait();
                }

                aux = 0;
            }
            else
            {
                aux = 1;
            }
            return resultado;
        }


        public static Task Delay(double tempo)
        {
            var s = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(tempo));
                return 42;
            });
            return s;
        }

        public static List<Rectangle> AlreadyToWork()
        {
            List<Rectangle> Resultado = new List<Rectangle>();


            using (Bitmap ThreedScreen = new Bitmap(pathOrigem + @"\ScreenSave\ScreenShot.bmp"))
            {
                Rectangle location2 = new Rectangle();
                Console.WriteLine("QUANTIDADE: " + First.Count());
                Console.WriteLine("QUANTIDADE: " + Second.Count());

                int x = 0, y = 0;
                for (int i = 0; i < First.Count(); i++)
                {
                    for (y = First[i].Y; y < (First[i].Y + First[i].Height); y++)
                    {
                        for (int u = 0; u < Second.Count(); u++)
                        {
                            for (x = Second[u].Y; x < (Second[u].Y + Second[u].Height); x++)
                            {
                                if (x == y)
                                {
                                    for (int p = 0; p < ThreedScreen.Width; p++)
                                    {
                                        for (int o = 0; o < ThreedScreen.Height; o++)
                                        {
                                            if (p >= Second[u].X && o >= Second[u].Y)
                                            {
                                                if (o <= Second[u].Y + Second[u].Height)
                                                {
                                                    if (p <= Second[u].X + Second[u].Width)
                                                    {
                                                        if (p == Convert.ToInt32((Second[u].X + (Second[u].Width / 2))) && o == Convert.ToInt32((Second[u].Y + (Second[u].Height / 2))))
                                                        {
                                                            location2.X = Second[u].X;
                                                            location2.Y = Second[u].Y;
                                                            Resultado.Add(location2);
                                                            Second.RemoveAt(u);
                                                            aux2 = true;
                                                        }
                                                    }
                                                }
                                            }
                                            if (aux2) break;
                                        }
                                        if (aux2) break;
                                    }
                                }

                                if (aux2) break;
                            }
                            if (aux2) break;
                        }
                        aux2 = false;
                    }
                }
                return Resultado;
            }
        }
    }
}
