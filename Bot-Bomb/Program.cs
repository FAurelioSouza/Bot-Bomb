using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;//                     A
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using Bot_Bomb.Controllers;

namespace Bot_Bomb
{
    public static class Program
    {
        static void Main(string[] args)
        {
            start();
        }
        public static void start()
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(10.0));
                return 42;
            });

            int aux = 0;

            Console.WriteLine("Começa em 10 segundos, coloque na tela do bomb");
            t.Wait();
            Console.WriteLine("Começou");

            CaptureController.FocusProcess();
            CaptureController.screen();
            Bitmap tela = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\telabombHeroes.jpg", true);
            Bitmap voltar = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\btnVoltar.jpg", true);
            Bitmap hunt = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\treasureHunt.jpg", true);


            while (true)
            {
                switch (aux)
                {
                    case 0:
                        if (MouseController.FindLocation(tela, voltar))
                        {
                            Console.WriteLine("Achou");
                        }
                        else
                        {
                            Console.WriteLine("Não Achou");
                        }
                        aux++;
                        break;
                    case 1:
                        if (MouseController.FindLocation(tela, hunt))
                        {
                            Console.WriteLine("Achou");
                        }
                        else
                        {
                            Console.WriteLine("Não Achou");
                        }
                        aux = 0;
                        break;
                }
                t.Wait();
            }


        }
    }
}


