using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public static class ConcurrentStackSample
    {
        // ConcurrentStack is a thread-safe FIFO (First In First Out) collection.
        public static ConcurrentStack<string> requestStack = new ConcurrentStack<string>();

        public static void Run()
        {
            //2 Start the requests stack monitoring thread
            Thread monitorThread = new Thread(MonitorQueue);
            monitorThread.Start();

            // 1. Enqueue Request
            StackRequest();
        }

        public static void StackRequest()
        {
            Console.WriteLine("Web Server Single Thread is running. Type 'exit' to stop.");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                requestStack.Push(input);
            }
        }

        public static void MonitorQueue()
        {
            while (true)
            {
                if (requestStack.Count > 0)
                {
                    if(requestStack.TryPop(out var input))
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
