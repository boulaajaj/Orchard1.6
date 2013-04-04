using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class WorldEdgeLine
    {
        public WorldEdgePoint WorldEdgePoint { get; set; }
        public string Key { get; set; }
    }
    public class CommandProcessorRobotWorld2 : CommandProcessor
    {
        private readonly IPersist<WorldEdgeLine> _store;

        public CommandProcessorRobotWorld2(ICommandInterpreter commandInterpreter, World world, IPersist<WorldEdgeLine> store)
            : base(commandInterpreter, world)
        {
            _store = store;
        }

        public override string ExecuteCommands(string commandsString, Robot robot)
        {
            foreach (var command in GetCommands(commandsString))
            {
                var queryBase=_store.GetAll().Where(x => x.WorldEdgePoint.Location.Point.X == robot.Location.Point.X &&
                                           x.WorldEdgePoint.Location.Direction == robot.Location.Direction &&
                                           x.WorldEdgePoint.Command.CommandInstruction == command.CommandInstruction);

              
                if(queryBase.Where(x => x.Key == "XMax").ToList().Any())
                    continue;

                if (queryBase.Where(x => x.Key == "YMax").ToList().Any())
                    continue;

                if (queryBase.Where(x => x.Key == "XMin").ToList().Any())
                    continue;

                if (queryBase.Where(x => x.Key == "YMin").ToList().Any())
                    continue;
                var currentLocation = robot.Location;              
                command.Execute(robot);
                var ret = RobotIsOnPlanet(robot);
                if (ret!=null)
                {
                    var worldEdge = new WorldEdgePoint() {Command = command, Location = currentLocation};
                    _store.Add(new WorldEdgeLine() { WorldEdgePoint = worldEdge,Key = ret});
                    return string.Format("{0} {1}", currentLocation, "LOST");
                }
            }

            return robot.Location.ToString();
        }

        public string RobotIsOnPlanet(Robot robot)
        {
            if (robot.Location.Point.X > _world.Point.X)
                return "XMax";
            if (robot.Location.Point.X < 0)
                return "XMin";
            if (robot.Location.Point.Y > _world.Point.Y)
                return "YMax";
            if (robot.Location.Point.Y < 0)
                return "YMin";

            return null;

        }
    }
}