using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class CommandProcessorRobotWorld : CommandProcessor
    {
        private readonly IPersist<WorldEdge> _store;

        public CommandProcessorRobotWorld(CommandInterpreter commandInterpreter, World world, IPersist<WorldEdge> store)
            : base(commandInterpreter, world)
        {
            _store = store;
        }

        public override string ExecuteCommands(string commandsString, Robot robot)
        {
            foreach (var command in GetCommands(commandsString))
            {
                var edges = _store.Query().Where(x => x.Location.Equals(robot.Location) && 
                                            x.Command.CommandInstruction == command.CommandInstruction).ToList();                
                if(edges.Count()>0)
                    continue;
                
                var currentLocation = robot.Location;              
                command.Execute(robot);
                if (!RobotIsOnPlanet(robot))
                {                    
                    _store.Add( new WorldEdge(){Command = command, Location = currentLocation});
                    return string.Format("{0} {1}", currentLocation, "LOST");
                }
            }

            return robot.Location.ToString();
        }

        public bool RobotIsOnPlanet(Robot robot)
        {
            if (robot.Location.Point.X > _world.Point.X)
                return false;
            if (robot.Location.Point.X < 0)
                return false;
            if (robot.Location.Point.Y > _world.Point.Y)
                return false;
            if (robot.Location.Point.Y < 0)
                return false;

            return true;

        }
    }
}