using AutoDrivingCar.Core.Constansts;
using System.Collections.Generic;
using System.Linq;

namespace AutoDrivingCar.Core
{
    public class Car
    {
        public static Position MoveForward(int maxX, int maxY, Position currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case Directions.North:
                    if (currentPosition.Y < maxY)
                    {
                        currentPosition.Y++;
                    }
                    break;
                case Directions.West:
                    if (currentPosition.X > 0)
                    {
                        currentPosition.X--;
                    }
                    break;
                case Directions.South:
                    if (currentPosition.Y > 0)
                    {
                        currentPosition.Y--;
                    }
                    break;
                case Directions.East:
                    if (currentPosition.X < maxX)
                    {
                        currentPosition.X++;
                    }
                    break;
                default:
                    break;
            }
            return currentPosition;
        }

        public static Position Rotates(Position currentPosition, string command)
        {
            switch (currentPosition.Direction)
            {
                case Directions.North:
                    currentPosition.Direction = command == Commands.Right ? Directions.East : Directions.West;
                    break;
                case Directions.West:
                    currentPosition.Direction = command == Commands.Right ? Directions.North : Directions.South;
                    break;
                case Directions.South:
                    currentPosition.Direction = command == Commands.Right ? Directions.West : Directions.East;
                    break;
                case Directions.East:
                    currentPosition.Direction = command == Commands.Right ? Directions.South : Directions.North;
                    break;
            }
            return currentPosition;
        }

        public static Position Move(int maxX, int maxY, Position currentPosition, string command)
        {
            if (command == Commands.Forward)
            {
                MoveForward(maxX, maxY, currentPosition);
            }
            else if (command == Commands.Right || command == Commands.Left)
            {
                Rotates(currentPosition, command);
            }
            currentPosition.Step++;
            return currentPosition;
        }

        public static Position CheckCollide(List<Position> carOne, List<Position> carTwo)
        {
            var count = carOne.Count() <= carTwo.Count() ? carOne.Count() : carTwo.Count();
            for (int i = 0; i < count; i++)
            {
                if (carOne[i].X == carTwo[i].X && carOne[i].Y == carTwo[i].Y)
                {
                    return new Position()
                    {
                        X = carOne[i].X,
                        Y = carOne[i].Y,
                        Step = i + 1
                    };
                }
            }
            return null;
        }
    }
}
