using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class DebugMulithreadSample
    {
        public static void Run() {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(Work);
                thread.Name = $"Thread {i}";
                Console.WriteLine($"{thread.Name}'s state is {thread.ThreadState}");
                thread.Start();
            }

            Thread.CurrentThread.Name = "Master thread";
            Work();

        }

        public static void Work()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} started working. The state is {Thread.CurrentThread.ThreadState}");
            Thread.Sleep(10000);
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} finished working. The state is {Thread.CurrentThread.ThreadState}");
        }
    }
}
