using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public static class ConcurrentQueuesSample
    {
        // ConcurrentQueue is a thread-safe FIFO (First In First Out) collection.
        public static ConcurrentQueue<string> requestQueue = new ConcurrentQueue<string>();

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
                    if(requestQueue.TryDequeue(out var input))
                    {
                        Thread processingThread = new Thread(() => ProcessInput(input));
                        processingThread.Start();
                    }
                }
                Thread.Sleep(100);
            }
        }

        public static void ProcessInput(string? input)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Processed input: {input}");
        }
    }
}
