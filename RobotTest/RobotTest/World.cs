namespace RobotTest
{
    public abstract class World
    {
        public readonly Point Point;

        protected World(Point point)
        {
            Point = point;
        }
    }
}