using System;
using Robots.Entities;
using Xunit;

namespace Robots.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_ValidateScript_IncorrectScriptThrows()
        {
            var worldReader = new WorldReader();
            Assert.Throws<ArgumentException>(() => worldReader.ValidateScript("q"));
        }

        [Fact]
        public void Test_ValidateScript_CorrectScriptsDoesntTrhowAndUpperize()
        {
            var worldReader = new WorldReader();
            Assert.Equal("FLFLRFF", worldReader.ValidateScript("flFLrFf"));
        }

        [Theory]
        [InlineData("1 2 ")]
        [InlineData("1 2")]
        public void Test_ParseWorld_Ok(string input)
        {
            var worldReader = new WorldReader();
            var world = worldReader.ParseWorld(input);
            Assert.Equal(1, world.MaxX);
            Assert.Equal(2, world.MaxY);
        }

        [Theory]
        [InlineData("1 2 3")]
        [InlineData("1 q")]
        [InlineData("qwerty")]
        public void Test_ParseWorld_WrongDataThrows(string input)
        {
            var worldReader = new WorldReader();
            Assert.Throws<Exception>(() => worldReader.ParseWorld(input));
        }

        [Theory]
        [InlineData("1 2 N", 1, 2, "N")]
        [InlineData("1 1 S", 1, 1, "S")]
        [InlineData("2 2 E", 2, 2, "E")]
        public void Test_ParseRobotInitData_Ok(string input, int x, int y, string orientation)
        {
            var world = new World(1, 1);
            var worldReader = new WorldReader();
            var robot = worldReader.ParseRobotInitData(world, input);
            Assert.Equal(new Position(x,y), robot.CurrentPosition);
            Assert.Equal(orientation, robot.Orientation.ToString());
        }

        [Theory]
        [InlineData("1 2 R")]
        [InlineData("1 1 l")]
        [InlineData("qwerty")]
        public void Test_ParseRobotInitData_WrongDataThrows(string input)
        {
            var world = new World(1, 1);
            var worldReader = new WorldReader();
            Assert.Throws<Exception>(() => worldReader.ParseRobotInitData(world, input));
        }
    }
}
