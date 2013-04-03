namespace RobotTest
{
    public class TurnRightCommand : Command
    {
       
        public override void Execute(Robot robot)
        {
            robot.TurnRight();
        }

        public override string CommandInstruction
        {
            get { return "R"; }
        }
    }
}