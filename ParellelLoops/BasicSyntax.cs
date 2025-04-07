using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    public static class BasicSyntax
    {
        public static void Run()
        {
            //int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] array = Enumerable.Range(1, 100).ToArray();

            int sum = 0;
            object lockObject = new object();

            Parallel.For(0, array.Length, i =>
            {
                // This is a thread-safe operation
                // The lock statement is used to ensure that only one thread can access the shared resource at a time
                lock (lockObject)
                {
                    sum += array[i];
                    Console.WriteLine($"Current Task id: {Task.CurrentId}, Is thread pool thread {Thread.CurrentThread.IsThreadPoolThread}");
                }

            });

            //Parallel.ForEach(array, i =>
            //{
            //    // This is a thread-safe operation
            //    // The lock statement is used to ensure that only one thread can access the shared resource at a time
            //    lock (lockObject)
            //    {
            //        sum += i;
            //    }
            //});

            // After the parellel loop line will execute after all the thread is completed
            Console.WriteLine($"The Sum is {sum}");
            Console.ReadLine();

            // Parallel.Invoke
            Parallel.Invoke(
                () => Console.WriteLine("Task 1"),
                () => Console.WriteLine("Task 2"),
                () => Console.WriteLine("Task 3")
            );

            Console.ReadLine();
        }
    }
}
