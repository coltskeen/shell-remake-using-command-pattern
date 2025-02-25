using src.receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.commands.concrete_commands
{
    /// <summary>
    /// The TypeCommand class implements the ICommand interface and
    /// encapsulates the command to show if the argument is a builtin or not.
    /// </summary>
    public class TypeCommand(Shell shell) : ICommand
    {
        // The receiver object that performs the actual action
        private readonly Shell _shell = shell;

        /// <summary>
        /// Initializes a new instance of the TypeCommand class with the specified receiver.
        /// </summary>
        /// <param name="shell">The receiver object that will execute the command.</param>
        public void Execute(string command) => _shell.Type(command);
    }
}
