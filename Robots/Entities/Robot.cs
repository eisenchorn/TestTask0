using System;
using Robots.Abstractions;

namespace Robots.Entities
{
    public class Robot
    {
        public Position CurrentPosition { get; private set; }
        public Orientation Orientation { get; private set; }
        public bool IsLost { get; private set; }
        IWorld _world;
        string _script;

        public Robot(IWorld world, int x, int y, Orientation orientation)
        {
            Orientation = orientation;
            CurrentPosition = new Position(x, y);
            _world = world;
        }

        public void LoadScript(string script)
        {
            _script = script;
        }

        public void Run()
        {
            foreach (var commandChar in _script)
            {
                if (IsLost)
                    return;
                else if (commandChar == 'R')
                    TurnRight();
                else if (commandChar == 'L')
                    TurnLeft();
                else if (commandChar == 'F')
                    MoveForward();
            }
        }

        public string Report()
        {
            return IsLost
            ? $"{CurrentPosition.X} {CurrentPosition.Y} {Orientation} LOST"
            : $"{CurrentPosition.X} {CurrentPosition.Y} {Orientation}";
        }

        private void MoveForward()
        {
            var nextPosition = GetFrontPosition();
            var nextPositionState = _world.CheckPosition(nextPosition);
            if (nextPositionState == PositionState.Scent)
                return;
            if (nextPositionState == PositionState.OutOfBounds)
            {
                _world.CreateScent(CurrentPosition);
                IsLost = true;
            }
            if (nextPositionState == PositionState.Ok)
            {
                CurrentPosition = nextPosition;
            }
        }

        private Position GetFrontPosition()
        {
            return Orientation.Forward(CurrentPosition);
        }
        private void TurnLeft()
        {
            Orientation = Orientation.TurnLeft();
        }
        private void TurnRight()
        {
            Orientation = Orientation.TurnRight();
        }
    }
}