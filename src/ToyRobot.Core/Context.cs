using System.IO;

namespace ToyRobot
{
    public sealed class Context
    {
        public Context(IRobert robert, TextWriter textWriter)
        {
            Robert = robert;
            TextWriter = textWriter;
        }

        public IRobert Robert { get; }

        public TextWriter TextWriter { get; }
    }
}
