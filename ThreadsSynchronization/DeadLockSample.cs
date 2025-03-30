using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    // e-commerce users and order
    // 1. Manange users ( user -> order )
    // 2. Manage orders ( order -> user )

    // Thread 1 want to lock user and then lock order
    // Thread 2 want to lock order and then lock user

    public static class DeadLockSample
    {
        private static object userLock = new object();
        private static object orderLock = new object();
        public static void Run()
        {
            // This sample will cause a deadlock beacuse no one is releasing the lock
            // We should avoid nested locks because it can cause deadlock

            Thread thread = new Thread(ManageOrder);
            thread.Start();

            ManageUser();

            thread.Join();

            Console.WriteLine("Program finished. Press any key to exit...");
            Console.ReadLine();
        }

        public static void ManageUser()
        {
       
            lock (userLock)
            {
                Console.WriteLine("User Management accquired the user lock");
                Thread.Sleep(2000);

                lock (orderLock)
                {
                    Console.WriteLine("User Management accquired the order lock");
                }
            }
        }

        public static void ManageOrder()
        {
            lock (orderLock)
            {
                Console.WriteLine("Order Management accquired the order lock");
                Thread.Sleep(1000);

                lock (userLock)
                {
                    Console.WriteLine("Order Management accquired the user lock");
                }
            }
        }
    }
}
