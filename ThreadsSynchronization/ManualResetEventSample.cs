using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    public static class ManualResetEventSample
    {
        public static ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
        public static void Run()
        {

            Console.WriteLine("Press enter to release all threads...");

            for (int i = 0; i < 3; i++)
            {
                Thread workerThread = new Thread(Work);
                workerThread.Name = $"Thread {i + 1}";
                workerThread.Start();
            }

            Console.ReadLine();

            manualResetEvent.Set();

            Console.ReadLine();

        }
        public static void Work()
        {
            
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the signal");
            manualResetEvent.Wait();
            Thread.Sleep(3000);
            Console.WriteLine($"{Thread.CurrentThread.Name} has been release.");
        }
    }
}
