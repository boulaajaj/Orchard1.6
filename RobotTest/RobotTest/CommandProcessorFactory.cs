
namespace RobotTest
{    
    public class CommandProcessorFactory
    {
         public CommandProcessor BuildWorldEdgePointsProcessor(World world)
         {
             //DI allows addition of new commands
             var commandInterpreter = DependencyResolver.GetInstance<ICommandInterpreter>();
             var storage = DependencyResolver.GetInstance<IPersist<WorldEdgePoint>>();

             return new CommandProcessorRobotWorldPoints(commandInterpreter, world, storage);
         }

         public CommandProcessor BuildWorldEdgeLinesProcessor(World world)
         {
             //DI allows addition of new commands
             var commandInterpreter = DependencyResolver.GetInstance<ICommandInterpreter>();
             var storage = DependencyResolver.GetInstance<IPersist<WorldEdgeLine>>();

             return new CommandProcessorRobotWorldLines(commandInterpreter, world, storage);
         }
    }
}


