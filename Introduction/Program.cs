// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Introduction.Topics;

bool running = true;

do {
    Console.WriteLine("Select an option:");
    Console.WriteLine("A: Basic Thread Syntax");
    Console.WriteLine("B: Divide and Conquer Comparison Performance 1 Thread vs 4 Thread");
    Console.WriteLine("C: Web Server Single Thread");
    Console.WriteLine("D: Web Server Multi Thread");
    Console.WriteLine("E: Exit");

    Console.Write("Choice: ");
    string choice = Console.ReadLine().ToUpper();

    switch (choice)
    {
        case "A":
            BasicThreadSyntax.Run();
            break;
        case "B":
            DivideConquer.Run();
            break;
        case "C":
            WebServerSingleThread.Run();
            break;
        case "D":
            WebServerMultiThread.Run();
            break;
        case "E":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option selected.");
            break;
    }
}
while (running);
