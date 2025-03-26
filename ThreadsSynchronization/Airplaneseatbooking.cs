using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsSynchronization
{
    class Airplaneseatbooking
    {
        public static Queue<string> requestQueue = new Queue<string>();
        public static int availableTicket = 10;
        public static object ticketlock = new object();

        public static void Run()
        {
            //2 Start the requests queue monitoring thread
            Thread monitorThread = new Thread(MonitorQueue);
            monitorThread.Start();

            // 1. Enqueue Request
            EnquueRequest();
        }

        public static void EnquueRequest()
        {
            Console.WriteLine($"Server is running. \r\nType 'b' to book a ticket \r\nType 'c' to cancelled \r\nType 'exit' to stop.");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                requestQueue.Enqueue(input);
            }
        }

        public static void MonitorQueue()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    string? input = requestQueue.Dequeue();
                    //Thread processingThread = new Thread(() => ProcessBooking(input));
                    Thread processingThread = new Thread(() => ProcessBookingMonitor(input));
                    processingThread.Start();
                }
                Thread.Sleep(100);
            }
        }

        // lock
        public static void ProcessBooking(string? input)
        {
            Thread.Sleep(1000);
            lock (ticketlock)
            {
                if (input == "b")
                {
                    if (availableTicket > 0)
                    {
                        availableTicket--;
                        Console.WriteLine();
                        Console.WriteLine($"Your seat is booked. Available ticket: {availableTicket}");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("No ticket available");
                    }

                }
                else if (input == "c")
                {
                    if (availableTicket < 10)
                    {
                        availableTicket++;
                        Console.WriteLine();
                        Console.WriteLine($"Your seat is cancelled. Available ticket: {availableTicket}");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("No ticket to cancel");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }            
        }

        //Monitor
        public static void ProcessBookingMonitor(string? input)
        {
            //Thread.Sleep(1000);

            if (Monitor.TryEnter(ticketlock, 1000))
            {
                Thread.Sleep(3000);
                //Thread.Sleep(1000);
                try
                {
                    if (input == "b")
                    {
                        if (availableTicket > 0)
                        {
                            availableTicket--;
                            Console.WriteLine();
                            Console.WriteLine($"Your seat is booked. Available ticket: {availableTicket}");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No ticket available");
                        }

                    }
                    else if (input == "c")
                    {
                        if (availableTicket < 10)
                        {
                            availableTicket++;
                            Console.WriteLine();
                            Console.WriteLine($"Your seat is cancelled. Available ticket: {availableTicket}");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No ticket to cancel");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                finally
                {
                    Monitor.Exit(ticketlock);
                }                
            }
            else { 
                Console.WriteLine("The system is busy. Please wait");
            }
        }
    }
}
