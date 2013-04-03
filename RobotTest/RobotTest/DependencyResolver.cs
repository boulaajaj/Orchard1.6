using System.Collections.Generic;
using SimpleInjector;

namespace RobotTest
{
    public static class DependencyResolver
    {
        private static Container _container;

        static DependencyResolver()
        {
            Bootstrap();
        }

        private static void Bootstrap()
        {
            _container = new Container();

            // Register your types, for instance:      
            _container.Register<ICommandInterpreter, CommandInterpreter>();
            _container.Register<IPersist<Location, Command>, Persist>();            
            _container.RegisterAll<Command>(new ForwardCommand(), new TurnLeftCommand(), new TurnRightCommand());                        
        }

        public static TService GetInstance<TService>() where TService : class
        {
            // Create the container as usual.
            if (_container == null)
                Bootstrap();

            return _container.GetInstance<TService>();
        }

        public static IEnumerable<TService> GetAllInstances<TService>() where TService : class
        {
            // Create the container as usual.
            if (_container == null)
                Bootstrap();

            return _container.GetAllInstances<TService>();
        }
    }
}