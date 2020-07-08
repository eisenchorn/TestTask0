using Robots.Entities;
using Xunit;

namespace Robots.Tests
{
    public class WorldTests
    {
        [Theory]
        [InlineData(5, 5, 0, 0)]//lower left corner
        [InlineData(5, 5, 5, 5)]//upper right corner
        [InlineData(5, 5, 3, 3)]//center
        [InlineData(5, 5, 3, 5)]//upper border
        [InlineData(4, 5, 3, 0)]//lower border
        [InlineData(4, 5, 0, 3)]//left border
        [InlineData(4, 5, 4, 3)]//right border

        public void Test_CheckPosition_OkInNewWorldWithCorrectCoordinates(int maxX, int maxY, int x, int y)
        {
            var world = new World(maxX, maxY);
            var positionState = world.CheckPosition(new Position(x, y));
            Assert.Equal(PositionState.Ok, positionState);
        }

        [Theory]
        [InlineData(5, 5, -1, 3)]//lower bound of X
        [InlineData(5, 5, 6, 3)]//upper bound of X
        [InlineData(5, 5, 3, -1)]//lower bound of Y
        [InlineData(5, 5, 3, 7)]//upper bound of Y
        [InlineData(5, 5, 10, 12)]//Both upper bounds
        [InlineData(5, 5, -1, -5)]//Both lower bounds
        [InlineData(5, 5, 7, -5)]//upperX and lower y
        [InlineData(5, 5, -1, 7)]//lowerX and uppery
        public void Test_CheckPosition_OutOfBoundsWithTooSmallOrTooBigCoordinates(int maxX, int maxY, int x, int y)
        {
            var world = new World(maxX, maxY);
            Assert.Equal(PositionState.OutOfBounds, world.CheckPosition(new Position(x, y)));
        }

        [Fact]
        public void Test_CreateScent_BeforeOkAndAfterScents()
        {
            var world = new World(4,4);
            var position = new Position(2,2);
            Assert.Equal(PositionState.Ok, world.CheckPosition(position));
            world.CreateScent(position);
            Assert.Equal(PositionState.Scent, world.CheckPosition(position));
        }
    }
}