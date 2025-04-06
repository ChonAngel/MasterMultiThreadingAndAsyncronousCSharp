using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncAwait
{
    /*
    async Task MethodName(...)
    { 

        ...

        await SomeTask;    
    }
    */

    /*
    async Task<Type> FunctionName(...)
    {

        ...

        await SomeTask;

        return ObjectofType;
    }
    */

    public static class BasicSyntaxAsyncAwait
    {
        public static async Task RunAsync()
        {
            //await WorkAsync();

            Console.WriteLine($"1. Main thread id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Start to do work.");

            var data = await FetchDataAsync();

            Console.WriteLine($"Data is fetched: {data}.");

            Console.WriteLine($"2 Thread id: {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        public static async Task WorkAsync()
        { 
            await Task.Delay(1000);

            Console.WriteLine("Work is done.");
        }

        public static async Task<string> FetchDataAsync()
        {
            Console.WriteLine($"3 Thread id: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(1000);

            // after await keyword use different thread depend on thread pool

            Console.WriteLine($"4 Thread id: {Thread.CurrentThread.ManagedThreadId}");

            return "Complex Data";
        }
    }
}