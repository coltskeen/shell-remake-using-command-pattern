using System.Net;
using System.Net.Sockets;

// Allow execution of command a random number of times
while (true)
{
    Console.Write("$ ");
    // Wait for user input
    var command = Console.ReadLine();
    Console.WriteLine($"{command}: command not found");
}