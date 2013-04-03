using System.Collections.Generic;

namespace RobotTest
{
    public interface ICommandInterpreter
    {
        IEnumerable<Command> Interpret(string command);
    }
}