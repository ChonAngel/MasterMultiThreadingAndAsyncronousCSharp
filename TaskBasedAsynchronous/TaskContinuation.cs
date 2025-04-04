using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class TaskContinuation
    {
        public static void Run() 
        {
            Wait();

            WaitAll();

            //ResultSample();

            //Continuation();

            ContinuationWith();
        }

        public static void Wait() 
        {
            int sum = 0;

            Console.WriteLine(" Task Wait block Main Thread Until The Task Done");

            var task = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Delay(1000);
                    sum += i;
                }
            });

            task.Wait();


            Console.WriteLine($"The result is {sum}");
        }

        public static void WaitAll()
        {
            int sum1 = 0, sum2 = 0;

            Console.WriteLine(" Task Wait All block Main Thread Until The All Task Done");

            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Delay(1000);
                    sum1 += i;
                }
            });

            var task2 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Task.Delay(1000);
                    sum2 += i;
                }
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine($"The result is {sum1 + sum2}");
        }

        public static void ResultSample()
        {
            // When we call the Task.Result it blocking the current Thread until the task finished
            var url = "https://pokeapi.co/api/v2/pokemon";

            using var client = new HttpClient();
            
            var task = client.GetStringAsync(url);

            var result = task.Result;

            Console.WriteLine(result);
        }

        public static void Continuation()
        { 
        
        }

        public static void ContinuationWith()
        {
            var url = "https://pokeapi.co/api/v2/pokemon";

            using var client = new HttpClient();

            var task = client.GetStringAsync(url);

            // Do ContinueWith to not block the Main Thread
            task.ContinueWith(t => { 
                var result = t.Result;
                //Console.WriteLine(result);
                var doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                JsonElement results = root.GetProperty("results");
                JsonElement first = results[0];

                Console.WriteLine($"First pokemon name: {first.GetProperty("name")}");
                Console.WriteLine($"First pokemon url: {first.GetProperty("url")}");
            });

            // Main Thread 
            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }
    }
}
