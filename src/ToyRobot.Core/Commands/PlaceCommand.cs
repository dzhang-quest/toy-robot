using System;
using System.Text.RegularExpressions;

namespace ToyRobot.Commands
{
    public sealed class PlaceCommand : Command
    {
        private readonly static Regex CommandLinePattern = new Regex(@"PLACE (\d+),(\d+),(WEST|NORTH|SOUTH|EAST)");

        internal readonly static Func<string, Command> Parse = commandLine =>
        {
            var match = CommandLinePattern.Match(commandLine);
            if (!match.Success)
                return null;

            var position = new Position()
            {
                X = int.Parse(match.Groups[1].Value),
                Y = int.Parse(match.Groups[2].Value)
            };

            var direction = (Direction)Enum.Parse(
                typeof(Direction),
                match.Groups[3].Value,
                ignoreCase: true);

            return new PlaceCommand(position, direction);
        };

        public Direction Direction { get; }

        public Position Position { get; }

        public PlaceCommand(Position location, Direction direction)
        {
            Position = location;
            Direction = direction;
        }

        public override void Do(Context ctx)
        {
            ctx.Robert.Place(Position, Direction);
        }
    }
}
