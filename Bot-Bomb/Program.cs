using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using Bot_Bomb.Controllers;
using System.Reflection;

namespace Bot_Bomb
{
    public static class Program
    {
        static void Main(string[] args)
        {
            start();
        }
        async public static void start()
        {
            string pathOrigem = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
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
            //Bitmap voltar = new Bitmap(pathOrigem + @"\ScreenSave\btnVoltar.bmp");
            //Bitmap hunt = new Bitmap(pathOrigem + @"\ScreenSave\treasureHunt.bmp");

            CaptureController.screen();
            if (MouseController.FindLocationList(5, .45))
            {
                Console.WriteLine("Achou");
            }
            else
            {
                Console.WriteLine("Não Achou");
            }

            //while (true)
            //{
            //    Console.WriteLine("COMEÇOU DO LOOP");
            //    CaptureController.screen();

            //    switch (aux)
            //    {
            //        case 0:
            //            if (MouseController.FindLocation(0, 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = 2;
            //            Console.WriteLine("CASE 0 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case 1:
            //            if (MouseController.FindLocation(1, 0.7))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = 0;
            //            Console.WriteLine("CASE 1 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case 2:
            //            if (MouseController.FindLocation(2, 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = 4;
            //            Console.WriteLine("CASE 2 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case 3:
            //            if (MouseController.FindLocation(3, 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            if (MouseController.FindLocation(4, .4))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }

            //            aux = 1;
            //            Console.WriteLine("CASE 3 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case 4:
            //            if (MouseController.FindLocation(4, .4))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }

            //            aux = 3;
            //            Console.WriteLine("CASE 4 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;

            //    }
            //    Console.WriteLine("FINALIZOU O LOOP");
            //    Delay(5).Wait();
            //    Console.Clear();
            //}

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

    }
}


