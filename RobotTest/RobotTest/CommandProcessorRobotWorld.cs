using System.Collections.Generic;

namespace RobotTest
{
    public class CommandProcessorRobotWorld : CommandProcessor
    {
        private readonly IPersist<Location, Command> _store;

        public CommandProcessorRobotWorld(CommandInterpreter commandInterpreter, World world, IPersist<Location, Command> store)
            : base(commandInterpreter, world)
        {
            _store = store;
        }

        public override string ExecuteCommands(string commandsString, Robot robot)
        {            
            foreach (var command in GetCommands(commandsString))
            {
                if (_store.Fetch().ContainsKey(robot.Location) && _store.Fetch()[robot.Location].CommandInstruction == command.CommandInstruction)
                    continue; 

                var currentLocation = robot.Location;              
                command.Execute(robot);
                if (!RobotIsOnPlanet(robot))
                {                    
                    _store.Add(currentLocation, command);
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