//MISC may refer to functions or processes that are not directly related to the main tasks of the thread, 
//such as error handling, logging, thread status management, 
//or other supporting functions that are not part of the core processing in the program.

using MultithreadingMISC;



//// Debug Multithread sample
//DebugMulithreadSample.Run();


//// State of thread
//StateofThread.Run();

//// Make thread wait for sometime
//ThreadWait.Run();

//// Thread Result Return
//ThreadResult.Run();

//// Thread Cancelling
//ThreadCancelling.Run();


////Thread Pool Example
//ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxIOThread);
//Console.WriteLine($"Max worker Thread: {maxWorkerThreads} , Max I/O Threads: {maxIOThread}");
//ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableIOThread);
//Console.WriteLine($"Active worker Thread: {maxWorkerThreads - availableWorkerThreads} , Active I/O Threads: {maxIOThread - availableIOThread}");
//ThreadPoolSample.Run();

// Thread Exeption Handling Sample
ThreadExeptionHandlingSample.Run();