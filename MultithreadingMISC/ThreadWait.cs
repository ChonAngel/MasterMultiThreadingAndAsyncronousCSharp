using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class ThreadWait
    {
        
        public static void Run()
        {
            Console.WriteLine("Before Wait...");

            //// Thread.Sleep
            //Thread.Sleep(3000);

            //// Thread.SpinWait
            //Thread.SpinWait(3000);

            // SpinWait.SpinUntil
            SpinWait.SpinUntil(IsRunning, 10);

            Console.WriteLine("After Wait Finish...");
        }

        public static bool IsRunning() 
        { 
            Console.WriteLine("Press any key to stop wait...");
            Console.ReadLine();
            return true;
        }
       
    }
}
