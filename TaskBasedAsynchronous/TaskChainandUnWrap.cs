using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class TaskChainandUnWrap
    {
        public static void Run() 
        {
            var url = "https://pokeapi.co/api/v2/pokemon";

            using var client = new HttpClient();

            var taskListJson = client.GetStringAsync(url);

            // Do ContinueWith to not block the Main Thread
            var taskGetFirstUrl = taskListJson.ContinueWith(t => {
                var result = t.Result;
                //Console.WriteLine(result);
                var doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                JsonElement results = root.GetProperty("results");
                JsonElement firstPokemon = results[0];

                return firstPokemon.GetProperty("url").ToString();
                //Console.WriteLine($"First pokemon name: {firstPokemon.GetProperty("name")}");
                //Console.WriteLine($"First pokemon url: {firstPokemon.GetProperty("url")}");
            });

            //The reason for using unwrap in a Task Chain in C# relates to extracting or "unwrapping" values
            //within Task or Task<T>, especially when working with chains of tasks.
            //Each task in a chain may have a result that is another Task or Task<T>,
            //and you need to unwrap the inner task to access its result.
            var taskGetDetailJson = taskGetFirstUrl.ContinueWith(t => {
                var result = t.Result;
                return client.GetStringAsync(result);
            }).Unwrap();

            taskGetDetailJson.ContinueWith(t => 
            {
                var result = t.Result;
                var doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                Console.WriteLine($"Name: {root.GetProperty("name").ToString()}");
                Console.WriteLine($"Weight: {root.GetProperty("weight").ToString()}");
                Console.WriteLine($"Height: {root.GetProperty("height").ToString()}");
            });

            // Main Thread 
            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }
    }
}
