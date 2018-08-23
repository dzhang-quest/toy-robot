using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;
using ToyRobot.Commands;

namespace ToyRobot
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void ExampleA()
        {
            var commandLines = new string[] {
                "PLACE 0,0,NORTH",
                "MOVE",
                "REPORT"};
            var expectedOutput = "0,1,NORTH";

            TestExample(expectedOutput, commandLines);
        }

        [TestMethod]
        public void ExampleB()
        {
            var commandLines = new string[] {
                "PLACE 0,0,NORTH",
                "LEFT",
                "REPORT"};
            var expectedOutput = "0,0,WEST";

            TestExample(expectedOutput, commandLines);
        }

        [TestMethod]
        public void ExampleC()
        {
            var commandLines = new string[] {
                "PLACE 1,2,EAST",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT"};
            var expectedOutput = "3,3,NORTH";

            TestExample(expectedOutput, commandLines);
        }

        private static void TestExample(string expectedOutput, string[] commandLines)
        {
            // Given
            var writer = new Mock<TextWriter>();
            var tableDimension = new TableDimension(width: 5, hight: 5);
            var context = new Context(new Robert(tableDimension), writer.Object);

            // When
            commandLines
                .Select(CommandFactory.Parse)
                .ToList()
                .ForEach(command => command.Do(context));

            // Then
            writer.Verify(r => r.WriteLine(expectedOutput));
        }
    }
}
