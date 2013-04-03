
namespace RobotTest
{
    public class CommandProcessorFactory
    {
         public CommandProcessor Build(World world)
         {
             //DI allows addition of new commands
             var commandInterpreter = DependencyResolver.GetInstance<CommandInterpreter>();
             var storage = DependencyResolver.GetInstance<IPersist<Location, Command>>(); 

             return new CommandProcessorRobotWorld(commandInterpreter, world, storage);
         }
    }
}


