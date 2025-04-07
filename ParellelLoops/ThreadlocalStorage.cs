using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    public static class ThreadlocalStorage
    {
        public static void Run()
        {
            DateTime beginTime = DateTime.Now;

            int[] array = Enumerable.Range(1, 10000).ToArray();

            int sum = 0;
            object lockObject = new object();

            // Use ThreadLocal to store the local sum for each thread to redudce lock Object time
            Parallel.For(0,array.Length,() => 0, (i, state, tls) =>
            {
                // This is a thread-safe operation
                // The lock statement is used to ensure that only one thread can access the shared resource at a time
                tls += array[i];                
                return tls;
            },

            (tls) =>            {
                // This is a thread-safe operation
                // The lock statement is used to ensure that only one thread can access the shared resource at a time
                lock (lockObject)
                {
                    sum += tls;
                    Console.WriteLine($"Current Task id: {Task.CurrentId}");
                }
            });

            DateTime EndTime = DateTime.Now;

            Console.WriteLine($"The Sum is {sum}");

            Console.WriteLine($"The Time taken is {EndTime.Subtract(beginTime).TotalMilliseconds} ms");
            Console.ReadLine();
        }
    }
}
