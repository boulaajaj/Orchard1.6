using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly Dictionary<string, Command> _commands;

        public CommandInterpreter(IEnumerable<Command> commands)
        {
            _commands = new Dictionary<string, Command>();

            foreach (var command in commands)
            {
                _commands.Add(command.CommandInstruction, command);
            }            
        }

        public IEnumerable<Command> Interpret(string commands)
        {
            if(!(commands.Length<100))
                throw new ArgumentOutOfRangeException("commands cannot be larger than 100");

            var instructions = commands.ToCharArray();

            return (from instruction in instructions where _commands.ContainsKey(instruction.ToString()) select _commands[instruction.ToString()]).ToList();
        }
    }
}