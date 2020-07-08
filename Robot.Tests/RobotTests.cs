using Xunit;
using Moq;
using Robots.Abstractions;
using Robots.Entities;
namespace Robots.Tests
{
    public class RobotTests
    {
        /// <summary>
        ///  When world says that forward is ok    
        /// Checking that 
        ///     coordinates changed correctly
        ///     didn't become lost
        ///     orientation hasn't changed
        /// </summary>
        [Theory]
        [InlineData(1, 1, "N", 1, 2)]
        [InlineData(1, 1, "E", 2, 1)]
        [InlineData(1, 1, "S", 1, 0)]
        [InlineData(1, 1, "W", 0, 1)]
        public void Test_Run_PositionChangedCorrectlyAfterMovingForwardInCurrentOrientation(
            int startX,
            int startY,
            string orientationStr,
            int newX,
            int newY)
        {
            var worldMock = new Mock<IWorld>();
            worldMock.Setup(x => x.CheckPosition(It.IsAny<Position>())).Returns(PositionState.Ok);
            var world = worldMock.Object;
            var orientStartParseResult = Orientation.TryParse(orientationStr, out var orientationStart);
            var robot = new Robot(world, startX, startY, orientationStart);
            robot.LoadScript("F");
            robot.Run();

            Assert.False(robot.IsLost);
            Assert.Equal(new Position(newX, newY), robot.CurrentPosition);//coordinate check
            Assert.Equal(orientationStart, robot.Orientation);//orientation hasn't changed
        }

        /// <summary>
        ///  When world says that forward is bound
        /// Checking that
        ///     Scent created
        ///     IsLost set to true
        ///     Position hasn't changed
        ///     Orientation hasn't changed after becoming lost
        /// </summary>
        [Theory]
        [InlineData(1, 1, "N")]
        [InlineData(1, 1, "E")]
        [InlineData(1, 1, "S")]
        [InlineData(1, 1, "W")]
        public void Test_Run_LogicOfBecomingLost(int startX, int startY, string orientationStr)
        {
            var worldMock = new Mock<IWorld>();
            worldMock.Setup(x => x.CheckPosition(It.IsAny<Position>())).Returns(PositionState.OutOfBounds);
            worldMock.Setup(x => x.CreateScent(It.IsAny<Position>()));
            var world = worldMock.Object;
            var orientStartParseResult = Orientation.TryParse(orientationStr, out var orientationStart);
            var robot = new Robot(world, startX, startY, orientationStart);
            robot.LoadScript("FR");
            robot.Run();

            worldMock.Verify(x => x.CreateScent(It.IsAny<Position>()), Times.Once());
            Assert.True(robot.IsLost);
            Assert.Equal(new Position(startX, startY), robot.CurrentPosition);
            Assert.Equal(orientationStart, robot.Orientation);
        }


        /// <summary>
        /// When world says that forward scents
        /// Checking that
        ///     robot doesn't become lost
        ///     Position isn't changed
        ///     Orientation can be changed
        /// </summary>
        [Theory]
        [InlineData(1, 1, "N", "E", "FR")]
        [InlineData(1, 1, "E", "S", "FR")]
        [InlineData(1, 1, "S", "E", "FL")]
        [InlineData(1, 1, "W", "S", "FL")]
        public void Test_Run_LogicOfNotGoingWhenScents(int startX, int startY, string orientationStartStr, string orientationFinishStr, string script)
        {
            var worldMock = new Mock<IWorld>();
            worldMock.Setup(x => x.CheckPosition(It.IsAny<Position>())).Returns(PositionState.Scent);
            var world = worldMock.Object;
            var orientStartParseResult = Orientation.TryParse(orientationStartStr, out var orientationStart);
            var orientExpectedParseResult = Orientation.TryParse(orientationFinishStr, out var orientationFinish);
            var robot = new Robot(world, startX, startY, orientationStart);
            robot.LoadScript(script);
            robot.Run();

            Assert.False(robot.IsLost);
            Assert.Equal(new Position(startX, startY), robot.CurrentPosition);
            Assert.Equal(new Position(startX, startY), robot.CurrentPosition);
            Assert.Equal(orientationFinish, robot.Orientation);

        }
    }
}