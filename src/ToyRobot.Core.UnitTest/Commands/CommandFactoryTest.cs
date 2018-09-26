using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToyRobot.Commands
{
    [TestClass]
    public class CommandFactoryTest
    {
        [TestMethod]
        public void CommandFactory_ParseFailed()
        {
            // Given
            var invalidCommandLine = "Anything invalid";

            // When
            var actual = CommandFactory.Parse(invalidCommandLine);

            // Then
            Assert.IsInstanceOfType(actual, typeof(DumbCommand));
        }

        [TestMethod]
        public void CommandFactory_ParsePlace()
        {
            // Given
            var commandLine = "PLACE 2,1,NORTH";

            // When
            var actual = CommandFactory.Parse(commandLine);

            // Then
            Assert.IsInstanceOfType(actual, typeof(PlaceCommand));
            var expectedPosition = new Position(x: 2, y: 1);
            var expectedDirection = Direction.North;
            Assert.AreEqual(expectedPosition, ((PlaceCommand)actual).Position);
            Assert.AreEqual(expectedDirection, ((PlaceCommand)actual).Direction);
        }

        [TestMethod]
        public void CommandFactory_ParseLeft()
        {
            // Given
            var commandLine = "LEFT";

            // When
            var actual = CommandFactory.Parse(commandLine);

            // Then
            Assert.IsInstanceOfType(actual, typeof(TurnCommand));
            Assert.IsTrue(((TurnCommand)actual).IsLeft);
        }

        [TestMethod]
        public void CommandFactory_ParseReport()
        {
            // Given
            var commandLine = "REPORT";

            // When
            var actual = CommandFactory.Parse(commandLine);

            // Then
            Assert.IsInstanceOfType(actual, typeof(ReportCommand));
        }

        [TestMethod]
        public void CommandFactory_ParseMove()
        {
            // Given
            var commandLine = "MOVE";

            // When
            var actual = CommandFactory.Parse(commandLine);

            // Then
            Assert.IsInstanceOfType(actual, typeof(MoveCommand));
        }
    }
}
