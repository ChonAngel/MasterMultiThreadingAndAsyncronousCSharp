//MISC may refer to functions or processes that are not directly related to the main tasks of the thread, 
//such as error handling, logging, thread status management, 
//or other supporting functions that are not part of the core processing in the program.


for(int i = 0; i < 10; i++)
{
    Thread thread = new Thread(Work);
    //threads[i].Name = $"Thread {i}";
    //Console.WriteLine($"{threads[i].Name}'s state is {threads[i].ThreadState}");
    thread.Start();
}

Work();

void Work()
{
    Console.WriteLine($"Thread {Thread.CurrentThread.Name} started working. The state is {Thread.CurrentThread.ThreadState}");
    Thread.Sleep(10000);
    Console.WriteLine($"Thread {Thread.CurrentThread.Name} finished working. The state is {Thread.CurrentThread.ThreadState}");
}

//using MultithreadingMISC;

//StateofThread.Run();