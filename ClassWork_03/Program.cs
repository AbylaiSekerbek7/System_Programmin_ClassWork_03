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
        static Mutex mutex = new Mutex(false);

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
            mutex.WaitOne();

            Console.WriteLine($"{id}: Thread is started");

            // Попытка захвата мьютекса
            
            // Hello My Name is Abylaikhan !
            Console.Write("Hello! ");       Thread.Sleep(0);
            Console.Write("My ");           Thread.Sleep(0);
            Console.Write("Name ");         Thread.Sleep(0);
            Console.Write("is ");           Thread.Sleep(0);
            Console.Write("Abylaikhan ");   Thread.Sleep(0);
            Console.WriteLine("! ");        Thread.Sleep(0);
            Console.WriteLine($"{id}: Thread is exit");

            // Освободить мьютекс
            mutex.ReleaseMutex();
        }
    }
}
