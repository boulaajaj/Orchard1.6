
namespace RobotTest
{
    public class CommandProcessorFactory
    {
         public CommandProcessor Build(World world)
         {
             //DI allows addition of new commands
             var commandInterpreter = DependencyResolver.GetInstance<CommandInterpreter>();
             var storage = DependencyResolver.GetInstance<IPersist<WorldEdge>>(); 
             //var storage2 = DependencyResolver.GetInstance<IPersist<string, Edge>>(); 

             return new CommandProcessorRobotWorld(commandInterpreter, world, storage);
             //return new CommandProcessorRobotWorldEdge(commandInterpreter, world, storage2);
         }
    }
}


