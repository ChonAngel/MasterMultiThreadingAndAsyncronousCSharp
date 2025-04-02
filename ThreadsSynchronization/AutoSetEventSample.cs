using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    // AutoResetEvent is used to signal one waiting thread and Auto close the signal
    public static class AutoSetEventSample
    {
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public static void Run()
        {
             
            Console.WriteLine("Server is running. Type 'go' to proceed");

            for(int i= 0; i< 3; i++)
            {
                Thread workerThread = new Thread(Worker);
                workerThread.Name = $"Worker {i+1}"; 
                workerThread.Start();
            }

            while (true)
            {
                string Userinput = Console.ReadLine()??"";
                if (Userinput == "go")
                {
                    autoResetEvent.Set();

                }
            }
        }
        public static void Worker()
        {
            while (true)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the signal");

                autoResetEvent.WaitOne();                

                Console.WriteLine($"{Thread.CurrentThread.Name} Worker thread proceed");

                Thread.Sleep(3000);
            }
        }
    }
}
