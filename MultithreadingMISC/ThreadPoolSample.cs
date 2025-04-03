using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class ThreadPoolSample
    {
        public static Queue<string> requestQueue = new Queue<string>();

        public static void Run()
        {
            //2 Start the requests queue monitoring thread
            Thread monitorThread = new Thread(MonitorQueue);
            monitorThread.Start();

            // 1. Enqueue Request
            EnquueRequest();
        }

        public static void EnquueRequest()
        {
            Console.WriteLine("Web Server Single Thread is running. Type 'exit' to stop.");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                requestQueue.Enqueue(input);
            }
        }

        public static void MonitorQueue()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    string? input = requestQueue.Dequeue();

                    //Thread processingThread = new Thread(() => ProcessInput(input));
                    //processingThread.Start();

                    // Use Thread Pool Instread 
                    ThreadPool.QueueUserWorkItem(ProcessInput, input);
                }
                Thread.Sleep(100);
            }
        }

        private static void ProcessInput(object? input)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Processed input: {input} Is Thread Pool Thread: {Thread.CurrentThread.IsThreadPoolThread}");
        }
    }
}
