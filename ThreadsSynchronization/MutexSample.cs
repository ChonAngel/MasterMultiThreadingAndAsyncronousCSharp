using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//// Mutx Syntax --> Allow 1 thread to access the Crtical Section
///  Across diffrent processes such as file access
///  If does not use difference process, use other techniques such as lock monitor
//using (var mutex = new Mutex())
//{ 
//    mutex.WaitOne();
//    try 
//    { 
//        // Crtical Section
//    }
//    finally { }
//}


namespace ThreadsSynchronization
{
    public static class MutexSample
    {
        public static string filePath = "counter.txt";
        public static void Run()
        {
            using (var mutex = new Mutex(false, $"FileMutex:{filePath}"))
            { 
                for (int i = 0; i < 10000; i++)
                {
                    mutex.WaitOne();
                    try
                    {
                        int counter = ReadCounter(filePath);
                        counter++;
                        WriteCounter(filePath, counter);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }

                }            
            }

            Console.WriteLine("Process finished");
            Console.ReadLine();
        }
        public static int ReadCounter(string? filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    string counter = reader.ReadToEnd();
                    return  string.IsNullOrEmpty(counter) ? 0 : int.Parse(counter);
                }
            }
        }
        public static void WriteCounter(string? filePath, int counter)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(counter);
                }
            }
        }
    }
}
