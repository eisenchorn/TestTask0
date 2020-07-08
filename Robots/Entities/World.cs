using System;
using System.Collections.Generic;
using Robots.Abstractions;

namespace Robots.Entities
{
    public class World : IWorld
    {
        const byte MaxCoordValue = 50;
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        private HashSet<Position> _scents;
        public World(int maxX, int maxY)
        {
            if (maxX > MaxCoordValue)
                throw new ArgumentOutOfRangeException(nameof(maxX));
            if (maxY > MaxCoordValue)
                throw new ArgumentOutOfRangeException(nameof(maxY));
            MaxX = maxX;
            MaxY = maxY;
            _scents = new HashSet<Position>();
        }

        public PositionState CheckPosition(Position position)
        {
            if (_scents.Contains(position))
                return PositionState.Scent;
            else if (position.X > MaxX || position.X < 0 || position.Y > MaxY || position.Y < 0)
                return PositionState.OutOfBounds;
            else
                return PositionState.Ok;
        }

        public void CreateScent(Position position)
        {
            _scents.Add(position);
        }
    }
}