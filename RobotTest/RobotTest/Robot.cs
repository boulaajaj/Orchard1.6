using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotTest;

namespace RobotTest
{
    public struct Point
    {
        public Point(int x, int y)
        {
            if(x>50 || y>50)
                throw new ArgumentOutOfRangeException("Co-oords cannot be larger than 50");
            X = x;
            Y = y;
        }
        public int X;
        public int Y;
    }

    public struct Location
    {
        public Location(Point point, Direction direction)
        {
            Point = point;
            Direction = direction;
        }

        public Point Point;
        public Direction Direction;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Point.X, this.Point.Y, this.Direction);            
        }
    }


    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public class Robot
    {
        public Location Location;

        public Robot(Location startingLocation)
        {
            Location = startingLocation;                   
        }

        public void MoveForward()
        {
            switch (Location.Direction)
            {
                case Direction.North:
                    Location.Point.Y++;
                    break;
                case Direction.East:
                    Location.Point.X++;
                    break;
                case Direction.South:
                    Location.Point.Y--;
                    break;
                case Direction.West:
                    Location.Point.X--;
                    break;
            }

        }

        public void TurnLeft()
        {
            if ((int)Location.Direction == 0)
                Location.Direction = Direction.West;
            else
                Location.Direction--;
        }

        public void TurnRight()
        {
            if ((int)Location.Direction == 3)
                Location.Direction = Direction.North;
            else
                Location.Direction++;
        }
    }
}
