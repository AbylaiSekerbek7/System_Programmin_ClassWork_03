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

        static AutoResetEvent evnt = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            // создаем именованный глобальный объект мьютекса
            bool isCreate;
            Mutex global = new Mutex(true, "MyGlobalMutex", out isCreate);
            if (isCreate)
            {
                Console.WriteLine("This is first run of programm");
            }
            else
            {
                Console.WriteLine("Can not run one more copy of programm");
                //return;
                global.WaitOne();
            }

            for (int i = 0; i < 10; ++i)
            {
                ThreadPool.QueueUserWorkItem(ThRoutine, null);
            }
            evnt.Set();

            Console.ReadLine();
            Console.WriteLine("Main: Good bye...");
            global.ReleaseMutex();
        }

        static void ThRoutine(object param)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            //mutex.WaitOne();
            //mutex.WaitOne();
            evnt.WaitOne();

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
            //mutex.ReleaseMutex();
            //mutex.ReleaseMutex();
            evnt.Set();
        }
    }
}