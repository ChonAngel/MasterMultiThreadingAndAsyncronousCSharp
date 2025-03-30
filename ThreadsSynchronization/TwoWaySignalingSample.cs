using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    public static class TwoWaySignalingSample
    {
        private static Queue<int> _queue = new Queue<int>();
        private static ManualResetEventSlim consumeEvent = new ManualResetEventSlim(false);
        private static ManualResetEventSlim produceEvent = new ManualResetEventSlim(true); // true to start producing

        private static int consumerCount = 0;
        private static object lockConsumerCount = new object();

        public static void Run()
        {
            Thread[] consumerThreads = new Thread[3];

            for (int i = 0; i < 3; i++)
            {
                consumerThreads[i] = new Thread(Consume);
                consumerThreads[i].Name = $"Consumer {i + 1}";
                consumerThreads[i].Start();
            }

            while (true)
            {
                produceEvent.Wait();
                produceEvent.Reset(); // Turn Off Signal: I already produced I don't want to produce again untill queue is empty

                Console.WriteLine("To Produce, enter 'p'");
                var input = Console.ReadLine()??"";
                if (input.ToLower() == "p")
                {
                    for (int i = 1; i <= 10; i++)
                    {
                       _queue.Enqueue(i);
                        Console.WriteLine($"Produced {i}");
                    }
                }
                consumeEvent.Set();  // Turn On Signal
            }
        }

        public static void Consume()
        {
            while (true)
            {
                consumeEvent.Wait();

                //consumeEvent.Reset(); // If need to only one consumer but this sample not required

                while (_queue.TryDequeue(out int item))
                {
                    // work on the items produced
                    Thread.Sleep(500);
                    Console.WriteLine($"Consume {item} from thread: {Thread.CurrentThread.Name}");
                }

                lock (lockConsumerCount)
                {
                    consumerCount++;

                    if (consumerCount == 3)
                    {
                        consumeEvent.Reset(); // Turn off Signal: the consume event when all consumers consumed
                        consumerCount = 0;
                        produceEvent.Set();

                        Console.WriteLine("***********************************************************************");
                        Console.WriteLine("***************************** More Please *****************************");
                        Console.WriteLine("***********************************************************************");
                    }
                }
            }
        }
    }
}
