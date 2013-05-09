using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class CommandProcessorRobotWorldPoints : CommandProcessor
    {
        private readonly IPersist<WorldEdgePoint> _store;

        public CommandProcessorRobotWorldPoints(ICommandInterpreter commandInterpreter, World world, IPersist<WorldEdgePoint> store)
            : base(commandInterpreter, world)
        {
            _store = store;
        }

        public override string ExecuteCommands(string commandsString, Robot robot)
        {
            foreach (var command in GetCommands(commandsString))
            {
                var edges = _store.GetAll().Where(x => x.Location.Equals(robot.Location) && 
                                            x.Command.CommandInstruction == command.CommandInstruction).ToList();                
                if(edges.Any())
                    continue;
                
                var currentLocation = robot.Location;              
                command.Execute(robot);
                if (!RobotIsOnPlanet(robot))
                {                    
                    _store.Add( new WorldEdgePoint(){Command = command, Location = currentLocation});
                    return string.Format("{0} {1}", currentLocation, "LOST");
                }
            }

            return robot.Location.ToString();
        }

        public bool RobotIsOnPlanet(Robot robot)
        {
            if (robot.Location.Point.X > World.Point.X)
                return false;
            if (robot.Location.Point.X < 0)
                return false;
            if (robot.Location.Point.Y > World.Point.Y)
                return false;
            if (robot.Location.Point.Y < 0)
                return false;

            return true;

        }
    }
}