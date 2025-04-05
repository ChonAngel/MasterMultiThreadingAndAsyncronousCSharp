using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public static class ExceptionHandling
    {
        public static async Task RunAsync()
        {

            var tasks = new[]
           {
                Task.Run(() =>
                {
                    throw new InvalidOperationException("InValid operation");                    
                }),
                Task.Run(() =>
                {
                    throw new ArgumentNullException("Argument null");                    
                }),
                Task.Run(() =>
                {
                    throw new Exception("General Exception");                    
                })
            };

            // Throw an exception of the first exception in aggregate exception 
            await Task.WhenAll(tasks);

            Console.WriteLine("Please enter key to exit...");
            Console.ReadLine();
        }
    }
}
