using Robots.Entities;
using Xunit;

namespace Robots.Tests
{
    public class OrientationTests
    {
        [Fact]
        public void Test_Parse()
        {
            Assert.True(Orientation.TryParse("N", out var orientation));
            Assert.Equal(Orientation.N, orientation);
            Assert.True(Orientation.TryParse("E", out orientation));
            Assert.Equal(Orientation.E, orientation);
            Assert.True(Orientation.TryParse("S", out orientation));
            Assert.Equal(Orientation.S, orientation);
            Assert.True(Orientation.TryParse("W", out orientation));
            Assert.Equal(Orientation.W, orientation);
            Assert.False(Orientation.TryParse("Q", out orientation));
            Assert.Null(orientation);
        }

        [Theory]
        [InlineData(1, 1, "N", 1, 2)]
        [InlineData(1, 1, "E", 2, 1)]
        [InlineData(1, 1, "S", 1, 0)]
        [InlineData(1, 1, "W", 0, 1)]
        public void Test_Forward_CoordinatesAreCorrect(int startX, int startY, string orientatString, int newX, int newY)
        {
            var position = new Position(startX, startY);
            var parseResult = Orientation.TryParse(orientatString, out var orientation);
            Assert.True(parseResult);
            var expectedNewPosition = new Position(newX, newY);
            Assert.Equal(expectedNewPosition, orientation.Forward(position));
        }

        [Fact]
        public void Test_TurnRight_ReturnsCorrectValues()
        {
            Assert.Equal(Orientation.E, Orientation.N.TurnRight());
            Assert.Equal(Orientation.S, Orientation.E.TurnRight());
            Assert.Equal(Orientation.W, Orientation.S.TurnRight());
            Assert.Equal(Orientation.N, Orientation.W.TurnRight());
        }

        [Fact]
        public void Test_TurnLeft_ReturnsCorrectValues()
        {
            Assert.Equal(Orientation.W, Orientation.N.TurnLeft());
            Assert.Equal(Orientation.N, Orientation.E.TurnLeft());
            Assert.Equal(Orientation.E, Orientation.S.TurnLeft());
            Assert.Equal(Orientation.S, Orientation.W.TurnLeft());
        }
    }
}