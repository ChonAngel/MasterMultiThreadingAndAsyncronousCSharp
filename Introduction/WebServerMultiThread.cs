using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public static class WebServerMultiThread
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
                    Thread processingThread = new Thread(() => ProcessInput(input));
                    processingThread.Start();
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
