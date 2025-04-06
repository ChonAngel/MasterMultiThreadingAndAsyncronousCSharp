using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public static class Overview
    {
        public static void Run()
        {
            OutputFirstPokemonAsync();

            // Main Thread 
            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }   

    public static async Task OutputFirstPokemonAsync()
        {
            var url = "https://pokeapi.co/api/v2/pokemon";

            using var client = new HttpClient();

            var taskGetPokemonlist = client.GetStringAsync(url);

            var resonpse = await taskGetPokemonlist;

            var doc = JsonDocument.Parse(resonpse);
            JsonElement root = doc.RootElement;
            JsonElement results = root.GetProperty("results");
            JsonElement first = results[0];

            Console.WriteLine($"First pokemon name: {first.GetProperty("name")}");
            Console.WriteLine($"First pokemon url: {first.GetProperty("url")}");

        }
    }
}