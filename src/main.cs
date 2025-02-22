using System.Net;
using System.Net.Sockets;

// Create a random number for REPL loops
Random rnd = new();
int loops = rnd.Next(1, 10);

// Allow execution of command a random number of times
for (int i = 0; i < loops; i++)
{
    Console.Write("$ ");
    // Wait for user input
    var command = Console.ReadLine();
    Console.WriteLine($"{command}: command not found");
}