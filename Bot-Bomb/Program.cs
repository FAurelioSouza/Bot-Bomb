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

            string aux = "Voltar";

            Console.WriteLine("Começa em 10 segundos, coloque na tela do bomb");
            t.Wait();
            Console.WriteLine("Começou");

            CaptureController.screen();
            if (MouseController.FindLocationList("Stamina", 0.4))
            {
                Console.WriteLine("Achou");
            }
            else
            {
                Console.WriteLine("Não Achou");
            }

            Delay(5).Wait();

            CaptureController.screen();
            if (MouseController.FindLocationList("Work", 0.4))
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
            //        case "Voltar":
            //            if (MouseController.FindLocation("Voltar", 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = "Heroes";
            //            Console.WriteLine("CASE 0 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case "Hunt":
            //            if (MouseController.FindLocation("Hunt", 0.7))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = "Voltar";
            //            Console.WriteLine("CASE 1 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case "Heroes":
            //            if (MouseController.FindLocation("Heroes", 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = "Todos";
            //            Console.WriteLine("CASE 2 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case "Close":
            //            if (MouseController.FindLocation("Close", 0.5))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }
            //            aux = "Hunt";
            //            Console.WriteLine("CASE 3 FINALIZADO COUNTADOR = " + aux);
            //            Delay(5).Wait();
            //            break;
            //        case "Todos":
            //            if (MouseController.FindLocation("Todos", .4))
            //            {
            //                Console.WriteLine("Achou");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Não Achou");
            //            }

            //            aux = "Close";
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


