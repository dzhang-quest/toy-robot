using System;
using System.Linq;

namespace ToyRobot.Commands
{
    public static class CommandFactory
    {
        private readonly static Func<string, Command>[] parsers = new Func<string, Command>[] {
            TurnCommand.Parse,
            MoveCommand.Parse,
            PlaceCommand.Parse,
            ReportCommand.Parse,
            DumbCommand.Parse,
        };

        public static Command Parse(string commandLine)
        {
            return parsers
                .Select(parse => parse(commandLine))
                .Where(command => !(command is null))
                .First();
        }
    }
}
