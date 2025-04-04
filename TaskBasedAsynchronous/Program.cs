// *** Difference between Multithreading and Asynchronous ***//
// Multithreading --> Devide and Conquer Task 
// Asynchronous --> Offload the long running task such as I/O operation task

// Basic syntax Task

using TaskBasedAsynchronous;
//BasicSyntaxTask.Run();

// Task VS Thread
// Task is Promise that we're going to get task done sometime in the future 
// - Use thread pool by default, Return values, Easy Continuation, Better Exception Handling
// Thread is basic programming unit to run something in CPU

// Task Result
TaskReturnResult.Run();

// Task Continuation - Wait, WaitAll and Result
//TaskContinuation.Run();