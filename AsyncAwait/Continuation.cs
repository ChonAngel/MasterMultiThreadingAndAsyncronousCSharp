using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public static class Continuation
    {
        public static async Task RunAsync()
        {            

            using var client = new HttpClient();

            var pokemonListJson = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon");

            // Get the first pokemon url
            var doc = JsonDocument.Parse(pokemonListJson);
            JsonElement root = doc.RootElement;
            JsonElement results = root.GetProperty("results");
            JsonElement firstPokemon = results[0];

            string url = firstPokemon.GetProperty("url").ToString();

            // Get the first pokemon details info
            var getFirstPokemonDetailsJson = await client.GetStringAsync(url);

            // Get weight and height
            doc = JsonDocument.Parse(getFirstPokemonDetailsJson);
            root = doc.RootElement;
            Console.WriteLine($"Name: {root.GetProperty("name").ToString()}");
            Console.WriteLine($"Weight: {root.GetProperty("weight").ToString()}");
            Console.WriteLine($"Height: {root.GetProperty("height").ToString()}");

            // Main Thread 
            Console.WriteLine("The End of Program...");

            Console.ReadLine();
        }
    }
}
