// Parellel loops helps us to devide or conquer
// Parallel loops helps us to write multi-threading programming just write sequential programming

// What happend behind the scene
// 1. Data Partition
// 2. Mostly use thread pool thread
// 3. Make the best decision by itself
// Blocking call

// In proformance
// If small loop use sequential because the overhead of creating thread is more than the time taken to execute the loop
// If huge loop use parallel

using ParellelLoops;

//BasicSyntax.Run();

//ExceptionHandling.Run();

//Stop.Run();

//Break.Run();

//ParallelLoopResultSample.Run();

//LoopParaellelCancellation.Run();

ThreadlocalStorage.Run();

