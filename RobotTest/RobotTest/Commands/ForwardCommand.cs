namespace RobotTest
{
    public class ForwardCommand : Command
    {
       
        public override void Execute(Robot robot)
        {
            robot.MoveForward();
        }

        public override string CommandInstruction
        {
            get { return "F"; }
        }
    }
}