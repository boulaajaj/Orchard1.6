
namespace RobotTest
{    
    public class CommandProcessorFactory
    {
        public CommandProcessor BuildWorldEdgeProcessor<TProcessor>(World world) where TProcessor : CommandProcessor
         {
             var cmdProcessor = DependencyResolver.GetInstance<TProcessor>();
             cmdProcessor.World = world;
             return cmdProcessor;
         }

    }
}


