using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class TaskCancelling
    {
        private static CancellationTokenSource cts = null;
        private static CancellationToken token;
        public static void Run()
        {
            //Sample1();

            Sample2();
        }

        public static void Work() 
        {
            Console.WriteLine("Start Doing");
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine($"{DateTime.Now}");
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"User requested cancellation at iteration: {i}");
                    throw new OperationCanceledException();
                    //break;
                }

                Thread.SpinWait(3000000);
            }
            Console.WriteLine("Work is Done"); 
        }

        public static void Sample1()
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

        public static void Sample2()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(1000);

            var url = "https://pokeapi.co/api/v2/pokemon";
            using var client = new HttpClient();
            var task1 = client.GetStringAsync(url, source.Token);

            Console.WriteLine(task1.Result);
        }
       
    }
}
