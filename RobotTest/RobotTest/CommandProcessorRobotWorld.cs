//using System.Collections.Generic;

//namespace RobotTest
//{
//    public class Edge
//    {
//        public Location Location { get; set; }
//        public Command Command { get; set; }
//    }
//    public class CommandProcessorRobotWorldEdge : CommandProcessor
//    {
//        private readonly IPersist<Edge> _store;

//        public CommandProcessorRobotWorldEdge(CommandInterpreter commandInterpreter, World world, IPersist<string, Edge> store)
//            : base(commandInterpreter, world)
//        {
//            _store = store;
//        }

//        public override string ExecuteCommands(string commandsString, Robot robot)
//        {            
//            foreach (var command in GetCommands(commandsString))
//            {
//                if(_store.Fetch().ContainsKey("XMax"))
//                {
//                    if(_store.Fetch()["XMax"].Location.Point.X == robot.Location.Point.X)
//                    {
//                        if(_store.Fetch()["XMax"].Command.CommandInstruction==command.CommandInstruction)
//                            continue;                        
//                    }
//                }

//                if (_store.Fetch().ContainsKey("XMin"))
//                {
//                    if (_store.Fetch()["XMin"].Location.Point.X == robot.Location.Point.X)
//                    {
//                        if (_store.Fetch()["XMin"].Command.CommandInstruction == command.CommandInstruction)
//                            continue;
//                    }
//                }

//                if (_store.Fetch().ContainsKey("YMax"))
//                {
//                    if (_store.Fetch()["YMax"].Location.Point.Y == robot.Location.Point.Y)
//                    {
//                        if (_store.Fetch()["YMax"].Command.CommandInstruction == command.CommandInstruction)
//                            continue;
//                    }
//                }

//                if (_store.Fetch().ContainsKey("YMin"))
//                {
//                    if (_store.Fetch()["YMin"].Location.Point.Y == robot.Location.Point.Y)
//                    {
//                        if (_store.Fetch()["YMin"].Command.CommandInstruction == command.CommandInstruction)
//                            continue;
//                    }
//                }
               
//                var currentLocation = robot.Location;              
//                command.Execute(robot);
//                var ret = RobotIsOnPlanet(robot);
//                if (ret!=null)
//                {                    
//                    _store.Add(ret, new Edge(){Command = command, Location = currentLocation});
//                    return string.Format("{0} {1}", currentLocation, "LOST");
//                }
//            }

//            return robot.Location.ToString();
//        }

//        public string RobotIsOnPlanet(Robot robot)
//        {
//            if (robot.Location.Point.X > _world.Point.X)
//                return "XMax";
//            if (robot.Location.Point.X < 0)
//                return "XMin";
//            if (robot.Location.Point.Y > _world.Point.Y)
//                return "YMax";
//            if (robot.Location.Point.Y < 0)
//                return "YMin";

//            return null;

//        }
//    }
//}