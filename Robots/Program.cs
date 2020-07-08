using System;
using System.Collections.Generic;
using System.IO;
using Robots.Entities;

namespace Robots
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("SampleInput.txt"))
            {
                var reader = new WorldReader();
                var robotData = File.ReadAllText("SampleInput.txt");
                var strings = robotData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                var world = reader.ParseWorld(strings[0]);
                var robots = new List<Robot>();
                for (var i = 1; i < strings.Length; i += 2)
                {
                    var input = strings[i];
                    var robot = reader.ParseRobotInitData(world, input);
                    input = strings[i + 1];
                    robot.LoadScript(reader.ValidateScript(input));
                    robots.Add(robot);
                }
                foreach (var robot in robots)
                {
                    robot.Run();
                }
                foreach (var robot in robots)
                {
                    Console.WriteLine(robot.Report());
                }
            }
            else
            {
                var reader = new WorldReader();
                Console.WriteLine("No input data file was specified or file not found.\nEnter input data row by row manually");
                var input = Console.ReadLine();
                var world = reader.ParseWorld(input);
                var robots = new List<Robot>();
                do
                {
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                        continue;
                    var robot = reader.ParseRobotInitData(world, input);
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                        continue;
                    robot.LoadScript(reader.ValidateScript(input));
                    robots.Add(robot);
                } while (!string.IsNullOrEmpty(input));
                foreach (var robot in robots)
                {
                    robot.Run();
                }
                foreach (var robot in robots)
                {
                    Console.WriteLine(robot.Report());
                }
            }
        }
    }
}
