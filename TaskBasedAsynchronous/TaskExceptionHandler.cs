using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class TaskExceptionHandler
    {
        // Exception Handling in Task
        // 1. Exceptions in Tasks are hidden
        // 2. Using try catch doesn't work
        // 3. Exception are store in the task itself
        // 4. Multiple one cen be stored hence we can iterate them
        // 5. Using waits or result will make the stored exception thrown

        public static void Run() 
        {
            //Sample1();

            //Sample2();

            Sample3();
        }

        public static void Sample1()
        {
            var url = "https://pokeapi111.co/api/v2/pokemon";

            using var client = new HttpClient();

            var task1 = client.GetStringAsync(url);

            // Do ContinueWith to not block the Main Thread
            var task2 = task1.ContinueWith(t => {
                var result = t.Result;
                //Console.WriteLine(result);
                var doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                JsonElement results = root.GetProperty("results");
                JsonElement first = results[0];

                Console.WriteLine($"First pokemon name: {first.GetProperty("name")}");
                Console.WriteLine($"First pokemon url: {first.GetProperty("url")}");
            });

            Thread.Sleep(1000);
            Console.WriteLine(task1.Status);
            Console.WriteLine(task2.Status);

            if (task1.IsFaulted && task1.Exception != null)
            {
                foreach (var ex in task1.Exception.InnerExceptions)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }

            // Main Thread 
            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }

        public static void Sample2()
        {
            var tasks = new[]
            {
                Task.Run(() => 
                { 
                    throw new InvalidOperationException("InValid operation");
                    return 1;
                }),
                Task.Run(() =>
                {
                    throw new ArgumentNullException("Argument null");
                    return 2;
                }),
                Task.Run(() =>
                {
                    throw new Exception("General Exception");
                    return 3;
                })
            };

            //Task.WhenAll(tasks).ContinueWith(t => {
            //    if (t.IsFaulted && t.Exception != null)
            //    {
            //        foreach (var ex in t.Exception.InnerExceptions)
            //        {
            //            Console.WriteLine(ex.Message.ToString());
            //        }
            //    }
            //});

            //Using waits or result will make the stored exception thrown
            //var t = Task.WhenAll(tasks);
            //t.Wait();
            //var result = t.Result;

            Console.WriteLine("Please enter key to exit...");
            Console.ReadLine();
        }

        public static async Task Sample3() 
        {
            var url = "https://pokeapi111.co/api/v2/pokemon";

            try
            {
                using var client = new HttpClient();
                var result = await client.GetStringAsync(url);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }
    }
}
