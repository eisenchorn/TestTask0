using System;
using Robots.Entities;

namespace Robots
{
    public class WorldReader
    {
        public World ParseWorld(string input)
        {
            var splitted = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length != 2 || !int.TryParse(splitted[0], out var maxX) || !int.TryParse(splitted[1], out var maxY))
            {
                throw new Exception("Invalid parameters. 2 integer values divided by space expected.");
            }
            var world = new World(maxX, maxY);
            return world;
        }
        public Robot ParseRobotInitData(World world, string input)
        {
            var splitted = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length != 3
            || !int.TryParse(splitted[0], out var x)
            || !int.TryParse(splitted[1], out var y)
            || !Orientation.TryParse(splitted[2], out Orientation orientation))
            {
                throw new Exception("Invalid parameters. 2 integer values and one character(N,S,E,W) divided by space expected.");
            }
            return new Robot(world, x, y, orientation);
        }

        public string ValidateScript(string script)
        {
            string allowableLetters = "LRF";
            var normScript = script.ToUpper();
            foreach (char c in normScript)
            {
                if (!allowableLetters.Contains(c.ToString()))
                    throw new ArgumentException("Script contains invalid characters", nameof(script));
            }
            return normScript;
        }
    }
}