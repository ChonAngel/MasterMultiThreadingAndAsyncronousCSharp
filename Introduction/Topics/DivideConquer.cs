using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Topics
{
    public static class DivideConquer
    {
        public static int[] arr = Enumerable.Range(1, 200).ToArray();
        private static int sum = 0;

        public static void Run() 
        {
            // 1 thread
            SumWith1Thread();

            //4 thread
            SumWithMultiThread();
        }

        public static void SumWith1Thread()
        {
            sum = 0;
            var StartTime = DateTime.Now;
            for (int i = 0; i < arr.Length; i++)
            {
                Thread.Sleep(100);
                sum += arr[i];
            }
            var EndTime = DateTime.Now;

            var timespan = EndTime - StartTime;

            Console.WriteLine($"Sum with 1 thread: {sum}");
            Console.WriteLine($"Time it Take: {timespan.Milliseconds}");
        }

        public static void SumWithMultiThread() 
        {
            var StartTime = DateTime.Now;

            int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;
            int numofThread = 4;
            int segmentLength = arr.Length / numofThread;

            Thread[] threads = new Thread[numofThread];
            threads[0] = new Thread(() => sum1 = SumSegment(0, segmentLength));
            threads[1] = new Thread(() => sum2 = SumSegment(segmentLength, segmentLength * 2));
            threads[2] = new Thread(() => sum3 = SumSegment(segmentLength * 2, segmentLength * 3));
            threads[3] = new Thread(() => sum4 = SumSegment(segmentLength * 3, arr.Length));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            var EndTime = DateTime.Now;

            var timespan = EndTime - StartTime;

            Console.WriteLine($"Sum with 4 thread: {sum1 + sum2 + sum3 + sum4}");
            Console.WriteLine($"Time it Take: {timespan.Milliseconds}");

        }

        public static int SumSegment(int start, int end)
        {
            int segmentSum = 0;
            for (int i = start; i < end; i++)
            {
                Thread.Sleep(100);
                segmentSum += arr[i];
            }
            return segmentSum;
        }

    }
}
