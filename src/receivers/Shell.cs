using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace src.receivers
{
    /// <summary>
    /// The Shell class acts as the receiver in the Command Pattern.
    /// It contains methods to execute shell commands.
    /// </summary>
    public class Shell
    {
        /// <summary>
        /// Exits the application with the provided exit code.
        /// </summary>
        /// <param name="command">The command string containing the exit command and optional exit code.</param>
        public void Exit(string command)
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

        /// <summary>
        /// Outputs the provided command string after removing the "echo" keyword.
        /// </summary>
        /// <param name="command">The command string containing the "echo" keyword and the message to be echoed.</param>
        public void Echo(string command)
        {
            // Remove "echo " if it exists at the beginning
            if (command.StartsWith("echo "))
            {
                Console.WriteLine(command.Substring(5));
            }
            // Remove "echo" if it exists at the beginning
            else if (command.StartsWith("echo"))
            {
                Console.WriteLine(command.Substring(4));
            }
        }
    }
}
