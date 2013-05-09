using System.Collections.Generic;

namespace RobotTest
{
    public abstract class CommandProcessor
    {
        private readonly ICommandInterpreter _commandInterpreter;
        internal World World { get; set; }        

        protected CommandProcessor(ICommandInterpreter commandInterpreter, World world)
        {
            _commandInterpreter = commandInterpreter;
            World = world;
        }

        public abstract string ExecuteCommands(string commandsString, Robot robot);

        protected IEnumerable<Command> GetCommands(string commandsString)
        {
            return _commandInterpreter.Interpret(commandsString);
        }
    }
}