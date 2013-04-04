using System.Collections.Generic;

namespace RobotTest
{
    public abstract class CommandProcessor
    {
        private readonly ICommandInterpreter _commandInterpreter;
        protected readonly World _world;        

        protected CommandProcessor(ICommandInterpreter commandInterpreter, World world)
        {
            _commandInterpreter = commandInterpreter;
            _world = world;
        }

        public abstract string ExecuteCommands(string commandsString, Robot robot);

        protected IEnumerable<Command> GetCommands(string commandsString)
        {
            return _commandInterpreter.Interpret(commandsString);
        }
    }
}