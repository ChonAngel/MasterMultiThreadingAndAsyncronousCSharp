using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    public static class Overview
    {
        public static int Counter = 0;
        public static object LockObject = new object();

//#define Use1by1Threads

//#define UseLock

        public static void Run()
        {

#if Use1by1Threads
                Thread thread1 = new Thread(IncrementingEventCounter);
                thread1.Start();
                thread1.Join();

                Thread thread2 = new Thread(IncrementingEventCounter);
                thread2.Start();
                thread2.Join();
               
#else
            Thread thread1 = new Thread(IncrementingCounter);
                Thread thread2 = new Thread(IncrementingCounter);

                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();
                
#endif

            Console.WriteLine($"Counter: {Counter}");
        }

        public static void IncrementingCounter()
        {

            for (int i = 0; i < 100000; i++)
            {
                // Critical section
                // Result will be 200000
                //lock (LockObject)
                //{
                //    Counter++;
                //}

                // No Lock - critical section 
                // When we remove the lock, the counter will not be 200000
                Counter++;
            }
        }

        

    }
}
