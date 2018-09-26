using System;

namespace ToyRobot
{
    public interface IRobert
    {
        void TurnLeft();
        void TurnRight();
        void Move();
        void Report(Action<string> reportCallback);
        TableDimension TableDimension { get; }
        Position? CurrentPosition { get; }
        bool Placed { get; }
        Direction? CurrentDirection { get; }
        void Place(Position position, Direction direction);
    }

    public class Robert : IRobert
    {
        public Robert(TableDimension tableDimension)
        {
            TableDimension = tableDimension;
        }

        public TableDimension TableDimension { get; }

        public Position? CurrentPosition { get; private set; }

        public Direction? CurrentDirection { get; private set; }

        public bool Placed => CurrentDirection.HasValue && CurrentPosition.HasValue;

        public void Move()
        {
            if (Placed)
            {
                var position = CurrentPosition.Value;
                Position nextPosition = new Position();
                switch (CurrentDirection.Value)
                {
                    case Direction.North:
                        nextPosition = new Position(position.X, position.Y + 1);
                        break;
                    case Direction.East:
                        nextPosition = new Position(position.X + 1, position.Y);
                        break;
                    case Direction.South:
                        nextPosition = new Position(position.X, position.Y - 1);
                        break;
                    case Direction.West:
                        nextPosition = new Position(position.X - 1, position.Y);
                        break;
                }

                if (IsOnTable(TableDimension, nextPosition))
                {
                    CurrentPosition = nextPosition;
                }
            }
        }

        public void Place(Position position, Direction direction)
        {
            if (IsOnTable(TableDimension, position))
            {
                CurrentPosition = position;
                CurrentDirection = direction;
            }
        }

        public void TurnLeft()
        {
            if (Placed)
            {
                CurrentDirection = (Direction)(((int)CurrentDirection.Value + 3) % 4);
            }
        }

        public void TurnRight()
        {
            if (Placed)
            {
                CurrentDirection = (Direction)(((int)CurrentDirection.Value + 1) % 4);
            }
        }

        public void Report(Action<string> reportCallback)
        {
            if (Placed)
            {
                var position = CurrentPosition.Value;
                var direction = CurrentDirection.Value;
                var report = $"{position.X},{position.Y},{direction.ToString().ToUpper()}";
                reportCallback(report);
            }
        }


        private static bool IsOnTable(TableDimension tableDimension, Position position)
            => position.X >= 0
            && position.Y >= 0
            && position.X < tableDimension.Width
            && position.Y < tableDimension.Hight;
    }
}
