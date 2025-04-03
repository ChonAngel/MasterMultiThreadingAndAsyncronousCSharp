using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class ThreadResult
    {
        private static string? result = null;
        public static void Run()
        {
            Thread thread = new Thread(Work);
            thread.Start();
            thread.Join();

            // Thread can not return result, Have to make the share resource changed
            Console.WriteLine($"The result is {result}");
        }

        public static void Work() 
        {
            Console.WriteLine("Start Do something work.");
            Thread.Sleep(1000);

            result = "Here is the result";
        }
       
    }
}
