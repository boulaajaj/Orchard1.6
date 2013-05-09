
namespace RobotTest
{    
    public class CommandProcessorFactory
    {
        public CommandProcessor Build<TProcessor>(World world) where TProcessor : CommandProcessor
         {
            var cmdProcessor = DependencyResolver.GetInstance<TProcessor>();
             cmdProcessor.World = world;
             return cmdProcessor;
         }

    }
}


