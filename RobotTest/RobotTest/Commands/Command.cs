namespace RobotTest
{
    public abstract class Command
    {
        public abstract string CommandInstruction { get; }

        public abstract void Execute(Robot robot);

    }


}