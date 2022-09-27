using AutoDrivingCar.Core;
using System;
using System.Collections.Generic;

namespace AutoDrivingCar.PartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Position> carOne = new List<Position>();
            List<Position> carTwo = new List<Position>();

            Console.WriteLine("Please enter the field size:");
            var sizes = Console.ReadLine().Split(" ");

            Console.WriteLine("Please enter the current position of car A:");
            var current = Console.ReadLine().Split(" ");

            Console.WriteLine("Please enter the next commands of car A:");
            var commands = Console.ReadLine();

            var maxX = int.Parse(sizes[0]);
            var maxY = int.Parse(sizes[1]);

            var currentPosition = new Position()
            {
                X = int.Parse(current[0]),
                Y = int.Parse(current[1]),
                Direction = current[2].ToString()
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

            Console.WriteLine("Please enter the current position of car B:");
            current = Console.ReadLine().Split(" ");
            Console.WriteLine("Please enter the next commands of car B:");
            commands = Console.ReadLine();

            currentPosition = new Position()
            {
                X = int.Parse(current[0]),
                Y = int.Parse(current[1]),
                Direction = current[2].ToString()
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
            if (collision != null)
            {
                Console.WriteLine($"Car A and car B will collide at ({collision.X},{collision.Y}) and at {collision.Step}th step:");
            }
            else
            {
                Console.WriteLine("No collision");
            }

            Console.ReadKey();
        }
    }
}
