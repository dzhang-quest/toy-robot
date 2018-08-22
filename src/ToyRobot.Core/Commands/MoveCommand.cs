using System;

namespace ToyRobot.Commands
{
    public sealed class MoveCommand : Command
    {
        internal readonly static Func<string, Command> Parse = commandLine =>
        {
            if (commandLine is "MOVE")
                return new MoveCommand();

            return null;
        };
    }
}
