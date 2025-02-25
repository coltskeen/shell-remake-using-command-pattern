using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.commands
{
    /// <summary>
    /// This is an interface that defines the method(s) for executing the command.
    /// </summary>
    public interface ICommand
    {
        void Execute(string command);
    }
}
