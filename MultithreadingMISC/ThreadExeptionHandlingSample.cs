using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingMISC
{
    public static class ThreadExeptionHandlingSample
    {
        private static List<Exception> exceptions = new List<Exception>();
        private static object lockException = new object();
        public static void Run()
        {
            Console.WriteLine("*** Thread Exception Handling ***");

            Thread thread = null;
            try { 
            thread = new Thread(() => {
                // We need to handle the exception within the thread
                try { 
                    throw new InvalidOperationException("Exception Occur");                
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                }
            });
            
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.ToString());   
            }

            thread.Start();
            thread.Join();

            Console.WriteLine("*** Add Thread Multi Error Exception ***");

            Thread thread1 = new Thread(Work);
            Thread thread2 = new Thread(Work);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            foreach (Exception ex in exceptions) 
            { 
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public static void Work()
        {
            try
            {
                throw new InvalidOperationException("Exception Occur");
            }
            catch (Exception ex)
            {
                lock(lockException)
                {
                    exceptions.Add(ex);
                }
            }
        }

    }
}
