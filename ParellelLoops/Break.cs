using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    public static class Break
    {
        public static void Run()
        {
            int[] array = Enumerable.Range(1, 100).ToArray();

            int sum = 0;
            object lockObject = new object();

            try
            {
                Parallel.For(0, array.Length, (i, state) =>
                {
                    lock (lockObject)
                    {

                        if(state.ShouldExitCurrentIteration && state.LowestBreakIteration < i)
                            return;
                        
                        // The thread avoid to run the remaining iterations
                        if (i == 65)
                        {
                            state.Break();
                        }

                        sum += array[i];
                        Console.WriteLine($"Current Task id: {Task.CurrentId}, Value: {array[i]}");
                        
                    }
                });
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Exception: " + ex.InnerException.Message);
            }

            // After the parellel loop line will execute after all the thread is completed
            Console.WriteLine($"The Sum is {sum}");
            Console.ReadLine();

        }
    }
}
