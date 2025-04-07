using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    public static class Cancellation
    {
        public static void Run()
        {
           var items = Enumerable.Range(1, 20);

            // Producure --> Buffer <-- Consumer
            // Producer does not produce the data until the consumer requests it

            //// Sequential LINQ
            //var evenNumbers = items.Where(i => i % 2 == 0);

            CancellationTokenSource cts = new CancellationTokenSource();

            // Producure
            // Parallel LINQ --> Improve performance when processing large data sets
            ParallelQuery<int> evenNumbers;
            
            evenNumbers = items
            .AsParallel()
            .WithMergeOptions(ParallelMergeOptions.FullyBuffered) // Buffer the results
            .WithCancellation(cts.Token) // Cancel the query
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
            try
            {
                evenNumbers.ForAll(i =>
                {
                    if (i > 8)
                    {
                        cts.Cancel(); // Cancel the query
                    }
                    Console.WriteLine($"{i}, Thread Id: {Thread.CurrentThread.ManagedThreadId}");
                });
            }
            catch (OperationCanceledException ex)
            {
                // Handle the exception
                Console.WriteLine($"Operation was canceled: {ex.Message}");
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    // Handle the exception
                    Console.WriteLine($"Exception: {e.Message}");
                    return true; // Indicate that the exception was handled
                });               
            }

        }
    }
}
