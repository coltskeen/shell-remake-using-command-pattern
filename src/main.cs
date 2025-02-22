using System.Net;
using System.Net.Sockets;

bool isExit = false;
int exitDefault = 0;


// Allow execution of command a random number of times
while (!isExit)
{
    Console.Write("$ ");
    // Wait for user input
    var command = Console.ReadLine();

    if (command == $"exit {exitDefault}".ToLower())
    {
        isExit = true;
    }
    else
    {
        Console.WriteLine($"{command}: command not found");
    }

}