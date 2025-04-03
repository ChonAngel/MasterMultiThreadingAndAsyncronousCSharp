using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public static class BasicThreadSyntax
    {
        public static void Run() {

            // If call main thread with other thread, main thread will use first thread id

            Thread thread1 = new Thread(WriteThreadId);
            Thread thread2 = new Thread(WriteThreadId);

            thread1.Name = "Thread 1";
            thread2.Name = "Thread 2";
            Thread.CurrentThread.Name = "Main Thread";

            thread1.Priority = ThreadPriority.Lowest;
            thread2.Priority = ThreadPriority.Highest;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            thread1.Start();
            thread2.Start();

            WriteThreadId();

            Console.ReadLine();
        }

        public static void WriteThreadId()
        {
            for (int i = 0; i < 100; i++)
            {
                //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(Thread.CurrentThread.Name);
                //Thread.Sleep(50);
            }
        }
    }
}
