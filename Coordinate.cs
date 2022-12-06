using System;
using System.Collections.Generic;
using System.Text;

namespace GridDivider
{
    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   X == coordinate.X &&
                   Y == coordinate.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public void Print()
        {
            Console.WriteLine("(" + X + "," + Y + ")");
        }

    }
}
