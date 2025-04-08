using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    public static class BasicPLINQ
    {
        public static void Run()
        {
           var items = Enumerable.Range(1, 200);

            // Producure --> Buffer --> Consumer
            // Producer does not produce the data until the consumer requests it

            //// Sequential LINQ
            //var evenNumbers = items.Where(i => i % 2 == 0);

            // Producure
            // Parallel LINQ --> Improve performance when processing large data sets
            var evenNumbers = items
                .AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered) // Buffer the results
                //.Order() // Order the items in parallel
                .Where(i =>
                {
                    Console.WriteLine($"Processing {i}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                    return i % 2 == 0;
                });

            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            //Console.WriteLine($"Even numbers Count: {evenNumbers.Count()}");

            //// Consumer
            ///Follow the producer buffer option
            //foreach (var number in evenNumbers)
            //{
            //    Console.WriteLine(number);
            //}

            // Consumer
            // ForAll --> Execute an action for each element in the collection
            // Although WithMergeOptions FullyBuffered is used, the order of the elements is not guaranteed
            // It use the same thread as the producer for loop through elements 
            evenNumbers.ForAll(i =>
            {
                Console.WriteLine($"{i}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            });

        }
    }
}
