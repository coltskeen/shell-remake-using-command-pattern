using System.Net;
using System.Net.Sockets;

// Allow execution of command a random number of times
while (true)
{
    Console.Write("$ ");
    // Wait for user input
    var command = Console.ReadLine();

    // If the exit command is provided
    if (command != null && command.StartsWith("exit"))
    {
        string[] cmdArgs = command.Split(" ");
        
        // And a code status is provided
        if (cmdArgs.Length > 1)
        {
            // Exit with that code status
            Environment.Exit(int.Parse(cmdArgs[1]));
        }
        else
        {
            // Else default to zero for the exit
            Environment.Exit(0);
        }
    }
    else
    {
        Console.WriteLine($"{command}: command not found");
    }

}


