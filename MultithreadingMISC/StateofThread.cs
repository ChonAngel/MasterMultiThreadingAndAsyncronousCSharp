using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class StateofThread
    {
        public static void Run() {
            Thread[] threads = new Thread[10];

            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(Work);
                threads[i].Name = $"Thread {i}";
                Console.WriteLine($"{threads[i].Name}'s state is {threads[i].ThreadState}");
                threads[i].Start();
            }

            Thread.Sleep(3000);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{threads[i].Name}'s state is {threads[i].ThreadState}");
            }

            Thread.CurrentThread.Name = "Master thread";
            Work();

            Thread.Sleep(3000);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{threads[i].Name}'s state is {threads[i].ThreadState}");
            }
        }

        public static void Work()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} started working. The state is {Thread.CurrentThread.ThreadState}");
            Thread.Sleep(10000);
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} finished working. The state is {Thread.CurrentThread.ThreadState}");
        }
    }
}
