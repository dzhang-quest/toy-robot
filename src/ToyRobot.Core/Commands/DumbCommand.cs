using System;

namespace ToyRobot.Commands
{
    /// <summary>
    /// A default, dumb command, which does nothing
    /// so client doesn't need to handle 'null' separately, 
    /// when a command is invalid.
    /// </summary>
    public sealed class DumbCommand: Command
    {
        internal readonly static Func<string, Command> Parse = commandLine => { return new DumbCommand(); };
    }
}
