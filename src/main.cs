using src.commands;
using src.commands.concrete_commands;
using src.invokers;
using src.receivers;
using System.Net;
using System.Net.Sockets;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            // Allow execution of command a random number of times
            while (true)
            {
                Console.Write("$ ");
                // Wait for user input
                var command = Console.ReadLine();

                Shell shell = new Shell();
                
                ICommand shellExit = new ExitCommand(shell);
                ICommand shellEcho = new EchoCommand(shell);
                ICommand shellType = new TypeCommand(shell);

                User user = new User();

                // If the exit command is provided
                if (command != null && command.StartsWith("exit"))
                { 
                    user.SetCommand(shellExit);
                    user.Enter(command);            
                }
                // If the echo command is provided
                else if (command != null && command.StartsWith("echo "))
                {
                    user.SetCommand(shellEcho);
                    user.Enter(command);
                }
                // If the type command is provided
                else if (command != null && command.StartsWith("type "))
                {
                    user.SetCommand(shellType);
                    user.Enter(command);
                }
                else
                {
                    Console.WriteLine($"{command}: command not found");
                }
            }
        }
    }
}



