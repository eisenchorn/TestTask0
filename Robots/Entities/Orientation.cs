using System.Collections.Generic;
using System;
using System.Linq;

namespace Robots.Entities
{
    public class Orientation
    {
        private int _incrX;
        private int _incrY;
        private string _name;
        public static Orientation W = new Orientation(-1, 0, "W");
        public static Orientation E = new Orientation(1, 0, "E");
        public static Orientation N = new Orientation(0, 1, "N");
        public static Orientation S = new Orientation(0, -1, "S");

        private static List<Orientation> TurningList = new List<Orientation>
        {
            Orientation.W,
            Orientation.N,
            Orientation.E,
            Orientation.S
        };

        // public static Dictionary<string, Orientation> Orientations = new Dictionary<string, Orientation>
        // {
        //     {"W", Orientation.W},
        //     {"N", Orientation.N},
        //     {"E", Orientation.E},
        //     {"S", Orientation.S}
        // };

        private Orientation(int incrX, int incrY, string name)
        {
            _incrX = incrX;
            _incrY = incrY;
            _name = name;
        }

        public Position Forward(Position pos)
        {
            return new Position(pos.X + this._incrX, pos.Y + this._incrY);
        }

        public Orientation TurnLeft()
        {
            var index = TurningList.IndexOf(this);
            return TurningList[(--index + 4) % TurningList.Count];
        }
        public Orientation TurnRight()
        {
            var index = TurningList.IndexOf(this);
            return TurningList[(++index) % TurningList.Count];
        }

        public static bool TryParse(string input, out Orientation orientation)
        {
            orientation = TurningList.FirstOrDefault(x=>x._name.Equals(input, StringComparison.InvariantCultureIgnoreCase));
            return orientation != null;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}