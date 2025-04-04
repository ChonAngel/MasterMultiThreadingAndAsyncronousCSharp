using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBasedAsynchronous
{
    public static class TaskReturnResult
    {
        public static int[] arr = Enumerable.Range(1, 200).ToArray();
        private static int sum = 0;

        public static void Run()
        {
            //4 thread
            SumWithMultiThread();
        }

        public static void SumWithMultiThread()
        {
            var StartTime = DateTime.Now;

            int numofThread = 4;
            int segmentLength = arr.Length / numofThread;

            Task<int>[] tasks = new Task<int>[numofThread];
            tasks[0] = Task.Run(() => { return SumSegment(0, segmentLength); });
            tasks[1] = Task.Run(() => { return SumSegment(segmentLength, segmentLength * 2); });
            tasks[2] = Task.Run(() => { return SumSegment(segmentLength * 2, segmentLength * 3); });
            tasks[3] = Task.Run(() => { return SumSegment(segmentLength * 3, arr.Length); });

            //Console.WriteLine($"Sum is: {tasks[0].Result + tasks[1].Result + tasks[2].Result + tasks[3].Result}");
            Console.WriteLine($"Sum is: {tasks.Sum(t => t.Result)}");

            var EndTime = DateTime.Now;
            var timespan = EndTime - StartTime;

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
