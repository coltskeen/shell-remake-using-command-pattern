using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly List<string> Builtins = new List<string> { "exit", "echo", "type" };

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

        /// <summary>
        /// This method takes a command string as input and checks if the command is a shell builtin.
        /// If not, it checks the system PATH for an executable.
        /// </summary>
        /// <param name="command">The command string to be checked.</param>
        public void Type(string command)
        {
            // Split the command string into arguments
            string[] cmdArgs = command.Split(' ');

            // Check if the command is a shell builtin
            if (Builtins.Contains(cmdArgs[1]))
            {
                Console.WriteLine($"{cmdArgs[1]} is a shell builtin");
            }
            else
            {
                // Get the full path of the command if it exists in the system PATH
                string fullPath = GetFullPath(cmdArgs[1]);
                Console.WriteLine(fullPath != null ? $"{cmdArgs[1]} is {fullPath}" : $"{cmdArgs[1]}: not found");
            }
        }

        /// <summary>
        /// This method takes a command string as input, finds the full path of the executable,
        /// and runs the executable.
        /// </summary>
        /// <param name="command">The command string to be executed.</param>
        public void Executable(string command)
        {
            // Split the command string into arguments
            string[] cmdArgs = command.Split(' ');

            // Get the full path of the command if it exists in the system PATH
            //string fullPath = GetFullPath(cmdArgs[0]); --> // Commenting out for now. For some reason CodeCrafters isn't using the full path to run the exes but running from current directory.

            // Run the executable
            using Process process = new Process();
            process.StartInfo.FileName = cmdArgs[0];
            process.StartInfo.Arguments = string.Join(" ", cmdArgs.Skip(1).ToArray());
            process.Start();
        }

        
        public void Pwd()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// This method takes a file name and checks if it exists in the system PATH.
        /// </summary>
        /// <param name="fileName">The file name to be checked.</param>
        /// <returns>The full path of the file if found, otherwise null.</returns>
        private static string GetFullPath(string fileName)
        {
            // Check if the file exists in the current directory
            if (File.Exists(fileName))
            {
                return Path.GetFullPath(fileName);
            }

            // Get the system PATH and split it into individual paths
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);
            foreach (string path in paths)
            {
                // Combine the path with the file name and check if it exists
                string fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            // Return null if the file is not found
            return null;
        }
    }
}
