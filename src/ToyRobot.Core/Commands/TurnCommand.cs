using System;

namespace ToyRobot.Commands
{
    public sealed class TurnCommand : Command
    {
        internal readonly static Func<string, Command> Parse = commandLine =>
        {
            switch (commandLine)
            {
                case "LEFT": return new TurnCommand(isLeft: true);
                case "RIGHT": return new TurnCommand(isLeft: false);
                default: return null;
            }
        };

        public bool IsLeft { get; }
        public bool IsRight { get => !IsLeft; }

        public TurnCommand(bool isLeft)
        {
            IsLeft = isLeft;
        }
    }
}
