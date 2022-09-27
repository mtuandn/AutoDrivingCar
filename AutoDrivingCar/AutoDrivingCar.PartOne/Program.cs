using AutoDrivingCar.Core;
using System;

namespace AutoDrivingCar.PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the field size:");
            var sizes = Console.ReadLine().Split(" ");
            Console.WriteLine("Please enter the current position:");
            var current = Console.ReadLine().Split(" ");
            Console.WriteLine("Please enter the next commands:");
            var commands = Console.ReadLine();

            var maxX = int.Parse(sizes[0]);
            var maxy = int.Parse(sizes[1]);

            var currentPosition = new Position()
            {
                X = int.Parse(current[0]),
                Y = int.Parse(current[1]),
                Direction = current[2].ToString()
            };

            foreach (var command in commands)
            {
                var position = Car.Move(maxX, maxy, currentPosition, command.ToString());
                currentPosition = position;
            }

            Console.WriteLine($"Output: {currentPosition.X} {currentPosition.Y} {currentPosition.Direction}");
            Console.ReadKey();
        }
    }
}
