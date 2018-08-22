using System;

namespace ToyRobot.Commands
{
    public sealed class ReportCommand : Command
    {
        internal readonly static Func<string, Command> Parse = commandLine =>
        {
            if (commandLine is "REPORT")
                return new ReportCommand();

            return null;
        };
    }
}
