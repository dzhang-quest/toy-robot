using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ToyRobot.Commands
{
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void Move()
        {
            // Given
            var robert = new Mock<IRobert>();
            var target = new MoveCommand();

            // When
            target.Do(new Context(robert.Object, Console.Out));

            // Then
            robert.Verify(r => r.Move());
        }

        [TestMethod]
        public void Report()
        {
            // Given
            var robert = new Mock<IRobert>();
            robert.Setup(r => r.Placed).Returns(true);
            var target = new ReportCommand();

            // When
            target.Do(new Context(robert.Object, Console.Out));

            // Then
            robert.Verify(r => r.Report(It.IsAny<Action<string>>()));
        }

        [TestMethod]
        public void TurnLeft()
        {
            // Given
            var robert = new Mock<IRobert>();
            var target = new TurnCommand(isLeft: true);

            // When
            target.Do(new Context(robert.Object, Console.Out));

            // Then
            robert.Verify(r => r.TurnLeft());
        }

        [TestMethod]
        public void TurnRight()
        {
            // Given
            var robert = new Mock<IRobert>();
            var target = new TurnCommand(isLeft: false);

            // When
            target.Do(new Context(robert.Object, Console.Out));

            // Then
            robert.Verify(r => r.TurnRight());
        }


        [TestMethod]
        public void Place()
        {
            // Given
            var robert = new Mock<IRobert>();
            var target = new PlaceCommand(new Position(x: 1, y: 2), Direction.East);

            // When
            target.Do(new Context(robert.Object, Console.Out));

            // Then
            robert.Verify(r => r.Place(It.Is<Position>(p => p.X == 1 && p.Y == 2), Direction.East));
        }
    }
}
