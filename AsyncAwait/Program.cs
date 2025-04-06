// Overview of Async Await

// Everything after the awit
// keyword is considered a continuation. When the program runs to the await
// keyword, the calling thread is imediatly released, so that it is free to do other things.

// Return a value

// Throw exceptions of the task if there is any

// Manages the synchronization context

using AsyncAwait;

//Overview.Run();

//await BasicSyntaxAsyncAwait.RunAsync();

//await Continuation.RunAsync();

await ExceptionHandling.RunAsync();

//What is await do
// When compiler sees the await keyword, it generates a state machine that
// as sson as the first async method finishes it's going to use the move next method to move to the next state
// every time it runs to the await line, it captures the synchronized context that why we do't have synchronization context problems