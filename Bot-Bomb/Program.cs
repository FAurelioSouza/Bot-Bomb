using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //CaptureController.FocusProcess();
            //CaptureController.screen();
            //Bitmap tela = null;
            Bitmap voltar = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\btnVoltar.bmp");
            Bitmap hunt = new Bitmap(@"C:\Users\lipex\Documents\Repository\Bot-Bomb\Bot-Bomb\ScreenSave\treasureHunt.bmp");

            while (true)
            {
                var s = Task.Run(async delegate
                {
                    await Task.Delay(TimeSpan.FromSeconds(5.0));
                    return;
                });
                Console.WriteLine("COMEÇOU DO LOOP");
                CaptureController.screen();

                switch (aux)
                {
                    case 0:
                        if (MouseController.FindLocation(voltar, 0.5))
                        {
                            Console.WriteLine("Achou");
                        }
                        else
                        {
                            Console.WriteLine("Não Achou");
                        }
                        aux++;
                        Console.WriteLine("CASE 0 FINALIZADO COUNTADOR = " + aux);
                        break;
                    case 1:
                        if (MouseController.FindLocation(hunt, 0.7))
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
                Console.WriteLine("FINALIZOU O LOOP");
                s.Wait();
            }

        }

    }
}


