using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClassWork_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                Thread th = new Thread(() => { ThRoutine(null); });
                th.Start();
            }

            Console.ReadLine();
            Console.WriteLine("Main: Good bye...");
        }

        static void ThRoutine(object param)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"{id}: Thread is started");
            // Hello My Name is Abylaikhan !
            Console.Write("Hello! ");       Thread.Sleep(0);
            Console.Write("My ");           Thread.Sleep(0);
            Console.Write("Name ");         Thread.Sleep(0);
            Console.Write("is ");           Thread.Sleep(0);
            Console.Write("Abylaikhan ");   Thread.Sleep(0);
            Console.Write("! ");
            Console.Write($"{id}: Thread is exit");
        }
    }
}
