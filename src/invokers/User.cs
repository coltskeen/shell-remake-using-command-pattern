using src.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.invokers
{
    /// <summary>
    /// The User class acts as the invoker in the Command Pattern.
    /// It holds a reference to a command and can execute it.
    /// </summary>
    public class User
    {
        // The command to be executed
        private ICommand _command;

        /// <summary>
        /// Sets the command to be executed.
        /// </summary>
        /// <param name="command">The command to be set.</param>
        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Executes the command with the provided input.
        /// </summary>
        /// <param name="command">The input command string.</param>
        public void Enter(string command)
        {
            _command.Execute(command);
        }
    }
}
