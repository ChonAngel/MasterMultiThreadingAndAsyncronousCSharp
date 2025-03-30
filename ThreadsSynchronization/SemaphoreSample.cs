using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    // Semaphore is a synchronization primitive that limits the number of threads that can access a resource or pool of resources concurrently.
    // Protect overloading of server
    // Syntax
    //using SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 3, maxCount: 3);
    //semaphore.Wait();
    //    try
    //    {
    //        // Do work
    //    }
    //    finally
    //    {
    //semaphore.Release();
    //}

    public static class SemaphoreSample
    {

        public static Queue<string> requestQueue = new Queue<string>();

        public static object queueObject = new object();

        private readonly static SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 3, maxCount: 3);

        //// If need to use Semaphore with global scope (Cross Process) use Semaphore instead of SemaphoreSlim
        //private readonly static Semaphore semaphoreGlobal = new Semaphore(3, 3);

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

                lock (queueObject)
                {
                    requestQueue.Enqueue(input);                
                }
            }
        }

        public static void MonitorQueue()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    string? input;
                    lock (queueObject)
                    {
                        input = requestQueue.Dequeue();
                    }
                    semaphore.Wait();
                    Thread processingThread = new Thread(() => ProcessInput(input));
                    processingThread.Start();
                }
                Thread.Sleep(100);
            }
        }

        // Prossing the request
        public static void ProcessInput(string? input)
        {
            try
            {
                // Simulate processing time
                Thread.Sleep(1000);
                Console.WriteLine($"Processed input: {input}");
            }
            finally
            {
                var prevCount = semaphore.Release();
                Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} release the semaphore. Previos count is {prevCount}");
            }            
        }
    }
}
