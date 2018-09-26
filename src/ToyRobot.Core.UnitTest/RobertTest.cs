using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ToyRobot
{
    [TestClass]
    public class RobertTest
    {
        [TestMethod]
        public void Robert_PlacedOutOfTableX()
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 6, hight: 5));

            // When
            target.Place(new Position(x: 6, y: 4), Direction.North);

            // Then
            Assert.IsNull(target.CurrentPosition);
        }

        [TestMethod]
        public void Robert_PlacedOutOfTableY()
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 6, hight: 5));

            // When
            target.Place(new Position(x: 5, y: 5), Direction.North);

            // Then
            Assert.IsNull(target.CurrentPosition);
        }

        [TestMethod]
        public void Robert_PlacedOnTable()
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 5, hight: 5));

            // When
            target.Place(new Position(x: 4, y: 3), Direction.North);

            // Then
            Assert.IsNotNull(target.CurrentPosition);
            Assert.AreEqual(new Position(x: 4, y: 3), target.CurrentPosition.Value);
            Assert.AreEqual(Direction.North, target.CurrentDirection.Value);
        }

        [TestMethod]
        public void Robert_ReportPosition()
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 5, hight: 5));
            target.Place(new Position(x: 4, y: 3), Direction.North);

            // When
            string actual = null;
            target.Report(report => actual = report);

            // Then
            var expected = "4,3,NORTH";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Robert_TurnLeft()
        {
            var currentDirections = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
            var nextDirections = new Direction[] { Direction.West, Direction.North, Direction.East, Direction.South };

            var turn = new Action<IRobert>(r => r.TurnLeft());
            for (int i = 0; i < currentDirections.Length; i++)
            {
                TestDirection(currentDirections[i], nextDirections[i], turn);
            }
        }

        [TestMethod]
        public void Robert_TurnRight()
        {
            var currentDirections = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
            var nextDirections = new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };

            var turn = new Action<IRobert>(r => r.TurnRight());
            for (int i = 0; i < currentDirections.Length; i++)
            {
                TestDirection(currentDirections[i], nextDirections[i], turn);
            }
        }

        [TestMethod]
        public void Robert_MoveOnTable()
        {
            var directions = new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };
            var currentPositions = Enumerable.Repeat(new Position(x: 3, y: 3), 4).ToArray();
            var nextPositions = new Position[] { new Position(x: 4, y: 3), new Position(x: 3, y: 2), new Position(x: 2, y: 3), new Position(x: 3, y: 4) };

            for (int i = 0; i < directions.Length; i++)
            {
                TestMove(directions[i], currentPositions[i], nextPositions[i]);
            }
        }

        [TestMethod]
        public void Robert_MoveOffTable()
        {
            var directions = new Direction[] { Direction.East, Direction.South, Direction.West, Direction.North };
            var currentPositions = new Position[] { new Position(x: 4, y: 2), new Position(x: 2, y: 0), new Position(x: 0, y: 2), new Position(x: 2, y: 4) };

            for (int i = 0; i < directions.Length; i++)
            {
                TestMove(directions[i], currentPositions[i], currentPositions[i]);
            }
        }

        private static void TestMove(Direction direction, Position currentPosition, Position nextPosition)
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 5, hight: 5));
            target.Place(currentPosition, direction);

            // When
            target.Move();

            // Then
            Assert.AreEqual(nextPosition, target.CurrentPosition.Value);
            Assert.AreEqual(direction, target.CurrentDirection.Value);
        }

        private static void TestDirection(Direction currentDirection, Direction nextDirection, Action<IRobert> turn)
        {
            // Given
            IRobert target = new Robert(new TableDimension(width: 5, hight: 5));
            target.Place(new Position(x: 3, y: 3), currentDirection);

            // When
            turn(target);

            // Then
            Assert.IsNotNull(target.CurrentPosition);
            Assert.AreEqual(new Position(x: 3, y: 3), target.CurrentPosition.Value);
            Assert.AreEqual(nextDirection, target.CurrentDirection.Value);
        }
    }
}
