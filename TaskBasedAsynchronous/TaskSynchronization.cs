using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    // Use all same Thread Teachnique

    public static class TaskSynchronization
    {

        public static Queue<string> requestQueue = new Queue<string>();

        public static object queueObject = new object();

        private readonly static SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 3, maxCount: 3);

        //// If need to use Semaphore with global scope (Cross Process) use Semaphore instead of SemaphoreSlim
        //private readonly static Semaphore semaphoreGlobal = new Semaphore(3, 3);

        public static void Run()
        {
            //2 Start the requests queue monitoring thread
            Task monitorThread = new Task(MonitorQueue);
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
                    Task processingThread = new Task(() => ProcessInput(input));
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
