using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace src.receivers
{
    /// <summary>
    /// The Shell class acts as the receiver in the Command Pattern.
    /// It contains methods to execute shell commands.
    /// </summary>
    public class Shell
    {
        private static readonly List<string> Builtins = new List<string> { "exit", "echo", "type", "pwd" };

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
            string[] cmdArgs = GetCmdArgs(command);

            if (cmdArgs.Length > 0 && cmdArgs[0].StartsWith("echo"))
            {
                // Display the arguments
                for (int i = 1; i < cmdArgs.Length; i++)
                {
                    Console.WriteLine(cmdArgs[i]);
                }
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

        /// <summary>
        /// Prints the current working directory to the console.
        /// </summary>
        public void Pwd()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Changes the current working directory to the specified path.
        /// </summary>
        /// <param name="command">The command string to be executed.</param>
        public void Cd(string command)
        {
            // Split the command string into arguments
            string[] cmdArgs = command.Split(' ');
            string fullPath = Path.GetFullPath(cmdArgs[1]);
            string home = Environment.GetEnvironmentVariable("HOME");

            // Navigate to the requested directory using absolute paths
            if (Path.IsPathRooted(cmdArgs[1]) && Directory.Exists(cmdArgs[1]))
            {
                Directory.SetCurrentDirectory(cmdArgs[1]);
            }
            // Navigate to the requested directory using relative paths
            else if (fullPath != null && Directory.Exists(fullPath))
            {
                Directory.SetCurrentDirectory(fullPath);
            }
            // Navigate to the requested directory using ~
            else if (cmdArgs[1].Contains('~') && home != null)
            {
                fullPath = Path.Combine(home, cmdArgs[1].Trim('~', '/'));
                Directory.SetCurrentDirectory(fullPath);
            }
            else
            {
                Console.WriteLine($"{cmdArgs[0]}: {cmdArgs[1]}: No such file or directory");
            }
        }


        public void Cat(string command) 
        {
            // Extract the arguments
            string[] cmdArgs = GetCmdArgs(command);

            // Get the file path(s) - including relative paths
            for (int i = 1; i < cmdArgs.Length; i++)
            {
                string fullPath = Path.GetFullPath(cmdArgs[i]);
                string home = Environment.GetEnvironmentVariable("HOME");

                // Read if an absolute path
                if (Path.IsPathRooted(cmdArgs[i]) && File.Exists(cmdArgs[i]))
                {
                    Console.WriteLine(File.ReadAllText(fullPath));
                }
                // Read if a relative path
                else if (fullPath != null && File.Exists(fullPath))
                {
                    Console.WriteLine(File.ReadAllText(fullPath));
                }
                // Read if using HOME path (~)
                else if (cmdArgs[i].Contains('~') && home != null)
                {
                    fullPath = Path.Combine(home, cmdArgs[i].Trim('~', '/'));
                    Console.WriteLine(File.ReadAllText(fullPath));
                }
                //else
                //{
                //    Console.WriteLine($"{cmdArgs[0]}: {cmdArgs[i]}: No such file or directory");
                //}


            }

        }


        private string[] GetCmdArgs(string command)
        {
            // Throw exception if command is null
            ArgumentNullException.ThrowIfNull(command);

            // Regex pattern to look for the single quotes
            string pattern = @"'([^']*)'|(\S+)";
            MatchCollection matches = Regex.Matches(command, pattern);

            return matches.Select(match => 
                match.Groups[1].Success 
                     ? match.Groups[1].Value
                     : match.Groups[2].Value)
                .ToArray();
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
