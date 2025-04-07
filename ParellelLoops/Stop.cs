using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    public static class Stop
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
                        if (!state.IsStopped)
                        {
                            // Stop Same Exception handling 
                            if (i == 65)
                            {
                                state.Stop();
                            }

                            sum += array[i];
                            Console.WriteLine($"Current Task id: {Task.CurrentId}, Is thread pool thread {Thread.CurrentThread.IsThreadPoolThread}");
                        }
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
