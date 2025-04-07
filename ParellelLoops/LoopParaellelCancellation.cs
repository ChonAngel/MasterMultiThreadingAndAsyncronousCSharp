using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    public static class LoopParaellelCancellation
    {
        private static CancellationTokenSource cts = null;
        private static CancellationToken token;
        private static ParallelOptions option = new ParallelOptions();

        public static void Run()
        {
            cts = new CancellationTokenSource();
            token = cts.Token;

            var task = Task.Run(Work, token);

            Console.WriteLine("Press 'C' to cancel");
            var input = Console.ReadLine();

            if (input == "c")
            {
                cts.Cancel();
            }

            //task.Wait();

            Console.WriteLine($"Task Status is {task.Status}");

            Console.ReadLine();
        }

        public static void Work()
        {
            Console.WriteLine("Start Doing");

            ParallelOptions option = new ParallelOptions { CancellationToken = cts.Token };
            try
            {
                Parallel.For(0, 100000, option, i =>
                {
                    Console.WriteLine($"{DateTime.Now}");                
                    Thread.SpinWait(300000);
                });
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
            Console.WriteLine("Work is Done");
        }
              
    }
}
