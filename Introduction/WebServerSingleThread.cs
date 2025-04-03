using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public static class WebServerSingleThread
    {
        public static void Run()
        {
            Console.WriteLine("Web Server Single Thread is running. Type 'exit' to stop.");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                ProcessInput(input);
            }
        }

        public static void ProcessInput(string? input)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Processed input: {input}");
        }
    }
}
