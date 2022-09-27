using AutoDrivingCar.Core;
using AutoDrivingCar.Core.Constansts;
using System.Collections.Generic;
using Xunit;

namespace AutoDrivingCar.Test
{
    public class CarTest
    {
        [Theory]
        [InlineData(1, 2, Directions.East, 2, 2)]
        [InlineData(1, 2, Directions.North, 1, 3)]
        [InlineData(1, 2, Directions.South, 1, 1)]
        [InlineData(1, 2, Directions.West, 0, 2)]
        public void MoveForwardTest(int x, int y, string direction, int xExpected, int yExpected)
        {
            var maxX = 10;
            var maxY = 10;
            var position = new Position()
            {
                X = x,
                Y = y,
                Direction = direction
            };

            var result = Car.MoveForward(maxX, maxY, position);
            Assert.Equal(xExpected, result.X);
            Assert.Equal(yExpected, result.Y);
        }

        [Theory]
        [InlineData(Directions.North, Commands.Right, Directions.East)]
        [InlineData(Directions.North, Commands.Left, Directions.West)]
        [InlineData(Directions.West, Commands.Right, Directions.North)]
        [InlineData(Directions.West, Commands.Left, Directions.South)]
        [InlineData(Directions.South, Commands.Right, Directions.West)]
        [InlineData(Directions.South, Commands.Left, Directions.East)]
        [InlineData(Directions.East, Commands.Right, Directions.South)]
        [InlineData(Directions.East, Commands.Left, Directions.North)]
        public void RotatesTest(string direction, string command, string directionExpected)
        {
            var position = new Position()
            {
                Direction = direction
            };
            var result = Car.Rotates(position, command);
            Assert.Equal(directionExpected, result.Direction);
        }

        [Theory]
        [InlineData(1, 2, Commands.Forward, 1, 3, Directions.North)]
        [InlineData(2, 3, Commands.Left, 2, 3, Directions.West)]
        public void MoveTest(int x, int y, string command, int xExpected, int yExpected, string directionExpected)
        {
            var maxX = 10;
            var maxY = 10;
            var direction = Directions.North;
            var position = new Position()
            {
                X = x,
                Y = y,
                Direction = direction
            };
            var result = Car.Move(maxX, maxY, position, command);

            Assert.Equal(xExpected, result.X);
            Assert.Equal(yExpected, result.Y);
            Assert.Equal(directionExpected, result.Direction);
        }

        [Fact]
        public void CheckCollideTest_ReturnCollision()
        {

            List<Position> carOne = new List<Position>();
            List<Position> carTwo = new List<Position>();
            var maxX = 10;
            var maxY = 10;
            var commands = "FFRFFFFRRL";
            var currentPosition = new Position()
            {
                X = 1,
                Y = 2,
                Direction = Directions.North
            };

            foreach (var command in commands)
            {
                var position = Car.Move(maxX, maxY, currentPosition, command.ToString());
                currentPosition = position;
                carOne.Add(new Position()
                {
                    X = position.X,
                    Y = position.Y,
                    Step = position.Step,
                    Direction = position.Direction
                });
            }

            commands = "FFLFFFFFFF";
            currentPosition = new Position()
            {
                X = 7,
                Y = 8,
                Direction = Directions.West
            };

            foreach (var command in commands)
            {
                var position = Car.Move(maxX, maxY, currentPosition, command.ToString());
                currentPosition = position;
                carTwo.Add(new Position()
                {
                    X = position.X,
                    Y = position.Y,
                    Step = position.Step,
                    Direction = position.Direction
                });
            }

            var collision = Car.CheckCollide(carOne, carTwo);
            Assert.Equal(5, collision.X);
            Assert.Equal(4, collision.Y);
            Assert.Equal(7, collision.Step);
        }
        
        [Fact]
        public void CheckCollideTest_ReturnNoCollision()
        {

            List<Position> carOne = new List<Position>();
            List<Position> carTwo = new List<Position>();
            var maxX = 10;
            var maxY = 10;
            var commands = "FFRFFFFRRL";
            var currentPosition = new Position()
            {
                X = 1,
                Y = 2,
                Direction = Directions.North
            };

            foreach (var command in commands)
            {
                var position = Car.Move(maxX, maxY, currentPosition, command.ToString());
                currentPosition = position;
                carOne.Add(new Position()
                {
                    X = position.X,
                    Y = position.Y,
                    Step = position.Step,
                    Direction = position.Direction
                });
            }

            commands = "FFLFFFFFFF";
            currentPosition = new Position()
            {
                X = 2,
                Y = 4,
                Direction = Directions.South
            };

            foreach (var command in commands)
            {
                var position = Car.Move(maxX, maxY, currentPosition, command.ToString());
                currentPosition = position;
                carTwo.Add(new Position()
                {
                    X = position.X,
                    Y = position.Y,
                    Step = position.Step,
                    Direction = position.Direction
                });
            }

            var collision = Car.CheckCollide(carOne, carTwo);
            Assert.True(collision == null);
        }
    }
}
