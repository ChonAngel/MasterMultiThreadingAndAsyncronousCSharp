using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public static class BlockingCollectionSample
    {
        // ConcurrentQueue is a thread-safe FIFO (First In First Out) collection.
        public static ConcurrentQueue<string> requestQueue = new ConcurrentQueue<string>();
        // BlockingCollection is a thread-safe collection that provides blocking and bounding capabilities.
        // Upperbounding is the maximum number of items that can be added to the collection until the consumer starts consuming.
        public static BlockingCollection<string> blockingCollection = new BlockingCollection<string>(requestQueue,3);

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
                    blockingCollection.CompleteAdding();
                    break;
                }
                blockingCollection.Add(input);
                Console.WriteLine($"Added input: {input}, Queue Size {blockingCollection.Count}");
            }
        }

        public static void MonitorQueue()
        {
            foreach (var input in blockingCollection.GetConsumingEnumerable())
            {
                if (blockingCollection.IsAddingCompleted)
                    break;
                Thread processingThread = new Thread(() => ProcessInput(input));
                    processingThread.Start();
                Thread.Sleep(2000);
            }               
        }

        public static void ProcessInput(string? input)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Processed input: {input}");
        }
    }
}
