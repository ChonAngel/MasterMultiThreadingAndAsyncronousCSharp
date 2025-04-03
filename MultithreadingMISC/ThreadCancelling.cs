using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class ThreadCancelling
    {
        private static bool cancelThread = false;
        public static void Run()
        {
            Thread thread = new Thread(Work);
            thread.Start();

            Console.WriteLine("Press 'C' to cancel");
            var input = Console.ReadLine();

            if (input == "c")
            {
                cancelThread = true;
            }

            thread.Join();
            Console.ReadLine();
        }

        public static void Work() 
        {
            Console.WriteLine("Start Doing");
            for (int i = 0; i < 100000; i++)
            { 
                if (cancelThread)
                {
                    Console.WriteLine($"User requested cancellation at iteration: {i}");
                    break;
                }

                Thread.SpinWait(300000);
            }
            Console.WriteLine("Work is Done"); 
        }
       
    }
}
