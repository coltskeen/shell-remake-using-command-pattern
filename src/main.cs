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
                ICommand shellExecutable = new ExecutableCommand(shell);
                ICommand shellPwd = new PwdCommand(shell);
                ICommand shellCd = new CdCommand(shell);
                ICommand shellCat = new CatCommand(shell);

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
                // If the executable command is provided
                else if (command != null && command.Contains("exe"))
                {
                    user.SetCommand(shellExecutable);
                    user.Enter(command);
                }
                // If the pwd command is provided
                else if (command != null && command.StartsWith("pwd"))
                {
                    user.SetCommand(shellPwd);
                    user.Enter(command);
                }
                // If the cd command is provided
                else if (command != null && command.StartsWith("cd"))
                {
                    user.SetCommand(shellCd);
                    user.Enter(command);
                }
                // If the cat command is provided
                else if (command != null && command.StartsWith("cat"))
                {
                    user.SetCommand(shellCat);
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



