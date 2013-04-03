using System.Collections.Generic;

namespace RobotTest
{
    public abstract class CommandProcessor
    {
        private readonly CommandInterpreter _commandInterpreter;
        protected readonly World _world;        

        protected CommandProcessor(CommandInterpreter commandInterpreter, World world)
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