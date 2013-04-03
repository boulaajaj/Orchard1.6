namespace RobotTest
{
    public class TurnLeftCommand : Command
    {

        public override void Execute(Robot robot)
        {
            robot.TurnLeft();
        }

        public override string CommandInstruction
        {
            get { return "L"; }
        }
    }
}