using Robots.Entities;

namespace Robots.Abstractions
{
    public interface IWorld
    {
        void CreateScent(Position position);
        PositionState CheckPosition(Position position);
    }
}