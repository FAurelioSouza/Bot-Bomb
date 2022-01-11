﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Bot_Bomb.Controllers
{

    public class CaptureController
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow); //ShowWindow needs an IntPtr

        public static void FocusProcess()
        {
            IntPtr hWnd; //change this to IntPtr
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "Google Chrome")
                {
                    hWnd = pr.MainWindowHandle; //use it as IntPtr not int
                    ShowWindow(hWnd, 3);
                    SetForegroundWindow(hWnd); //set to topmost
                }
            }
        }

        public static void screen()
        {
            string path = @"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\ScreenShot.bmp";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    g.Dispose();
                }

                bitmap.Save(path, ImageFormat.Bmp);
                bitmap.Dispose();
                
            }
        }
        public static Rectangle searchBitmap(int operador, double tolerance)
        {
            string path = null;
            string tela = @"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\ScreenShot.bmp";
            if (operador == 0)
            {
                path = @"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\btnVoltar.bmp";
            }
            else
            {
                path = @"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\treasureHunt.bmp";
            }
            using (Bitmap bigBmp = new Bitmap(tela))
            {
                using (Bitmap smallBmp = new Bitmap(path))
                {
                    BitmapData smallData =
                         smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                               System.Drawing.Imaging.ImageLockMode.ReadOnly,
                               System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    BitmapData bigData =
                      bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                               System.Drawing.Imaging.ImageLockMode.ReadOnly,
                               System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    int smallStride = smallData.Stride;
                    int bigStride = bigData.Stride;

                    int bigWidth = bigBmp.Width;
                    int bigHeight = bigBmp.Height - smallBmp.Height + 1;
                    int smallWidth = smallBmp.Width * 3;
                    int smallHeight = smallBmp.Height;

                    Rectangle location = Rectangle.Empty;
                    int margin = Convert.ToInt32(255.0 * tolerance);

                    unsafe
                    {
                        byte* pSmall = (byte*)(void*)smallData.Scan0;
                        byte* pBig = (byte*)(void*)bigData.Scan0;

                        int smallOffset = smallStride - smallBmp.Width * 3;
                        int bigOffset = bigStride - bigBmp.Width * 3;

                        bool matchFound = true;

                        for (int y = 0; y < bigHeight; y++)
                        {
                            for (int x = 0; x < bigWidth; x++)
                            {
                                byte* pBigBackup = pBig;
                                byte* pSmallBackup = pSmall;

                                //Look for the small picture.
                                for (int i = 0; i < smallHeight; i++)
                                {
                                    int j = 0;
                                    matchFound = true;
                                    for (j = 0; j < smallWidth; j++)
                                    {
                                        //With tolerance: pSmall value should be between margins.
                                        int inf = pBig[0] - margin;
                                        int sup = pBig[0] + margin;
                                        if (sup < pSmall[0] || inf > pSmall[0])
                                        {
                                            matchFound = false;
                                            break;
                                        }

                                        pBig++;
                                        pSmall++;
                                    }

                                    if (!matchFound) break;

                                    //We restore the pointers.
                                    pSmall = pSmallBackup;
                                    pBig = pBigBackup;

                                    //Next rows of the small and big pictures.
                                    pSmall += smallStride * (1 + i);
                                    pBig += bigStride * (1 + i);
                                }

                                //If match found, we return.
                                if (matchFound)
                                {
                                    location.X = x;
                                    location.Y = y;
                                    location.Width = smallBmp.Width;
                                    location.Height = smallBmp.Height;
                                    break;
                                }
                                //If no match found, we restore the pointers and continue.
                                else
                                {
                                    pBig = pBigBackup;
                                    pSmall = pSmallBackup;
                                    pBig += 3;
                                }
                            }

                            if (matchFound) break;

                            pBig += bigOffset;
                        }
                    }

                    bigBmp.UnlockBits(bigData);
                    smallBmp.UnlockBits(smallData);

                    smallBmp.Dispose();
                    bigBmp.Dispose();

                    return location;
                }
            }
            


        }
    }
}
