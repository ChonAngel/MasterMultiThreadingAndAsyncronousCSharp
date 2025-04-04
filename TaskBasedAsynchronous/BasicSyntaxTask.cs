using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class BasicSyntaxTask
    {
        public static void Run()
        {
            // Basic syntax Task

            //Thread thread = new Thread(Work);
            //thread.Start();

            // Syntax 1
            //Task task = new Task(Work);
            //task.Start();

            // Syntax 2
            //Task.Run(Work);

            // It can return
            var t = Task.Run(Work);

            // Use Task.Result to get result
            Console.WriteLine($"The result is {t.Result}");

            Console.ReadLine();
        }

        public static int Work()
        {
            Console.WriteLine("I love PG");
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);

            int result = 200;
            return result;
        }
    }
}
