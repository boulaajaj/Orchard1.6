using System;
using System.Collections.Generic;
using System.Diagnostics;

using NUnit.Framework;
using RobotTest;

namespace Robot.Tests
{
    [TestFixture]
    public class RobotTests
    {
        private readonly Point _worldCoords = new Point(5, 3);

        [Test]
        public void Perform_Full_Test_Using_Edge_Points()
        {
            var commandProcessorFactory = new CommandProcessorFactory();
            var commandProcessorRobotWorld = commandProcessorFactory.Build<CommandProcessorRobotWorldPoints>(new Mars(_worldCoords));

            var robot1 = new RobotTest.Robot(new Location(new Point(1, 1), Direction.East));
            var robot2 = new RobotTest.Robot(new Location(new Point(3, 2), Direction.North));
            var robot3 = new RobotTest.Robot(new Location(new Point(0, 3), Direction.West));

            var robotLocation1 = commandProcessorRobotWorld.ExecuteCommands("RFRFRFRF", robot1);
            var robotLocation2 = commandProcessorRobotWorld.ExecuteCommands("FRRFLLFFRRFLL", robot2);
            var robotLocation3 = commandProcessorRobotWorld.ExecuteCommands("LLFFFLFLFL", robot3);

            Assert.AreEqual("1 1 East", robotLocation1);
            Assert.AreEqual("3 3 North LOST", robotLocation2);
            Assert.AreEqual("2 3 South", robotLocation3);

            
        }

        /// <summary>
        /// This algorithm would scale better on a huge world (only 4 conditions to store)
        /// </summary>
        [Test]
        public void Perform_Full_Test_Using_Edge_Lines()
        {

            var commandProcessorFactory = new CommandProcessorFactory();
            var commandProcessorRobotWorld = commandProcessorFactory.Build<CommandProcessorRobotWorldLines>(new Mars(_worldCoords));

            var robot1 = new RobotTest.Robot(new Location(new Point(1, 1), Direction.East));
            var robot2 = new RobotTest.Robot(new Location(new Point(3, 2), Direction.North));
            var robot3 = new RobotTest.Robot(new Location(new Point(0, 3), Direction.West));

            var robotLocation1 = commandProcessorRobotWorld.ExecuteCommands("RFRFRFRF", robot1);
            var robotLocation2 = commandProcessorRobotWorld.ExecuteCommands("FRRFLLFFRRFLL", robot2);
            var robotLocation3 = commandProcessorRobotWorld.ExecuteCommands("LLFFFLFLFL", robot3);

            Assert.AreEqual("1 1 East", robotLocation1);
            Assert.AreEqual("3 3 North LOST", robotLocation2);
            Assert.AreEqual("2 3 South", robotLocation3);

        }

        [Test]
        public void subsequent_robots_should_not_fall_off_planet_at_same_point()
        {
            var commandProcessorFactory = new CommandProcessorFactory();
            var commandProcessorRobotWorld = commandProcessorFactory.Build<CommandProcessorRobotWorldPoints>(new Mars(_worldCoords));

            var robot = new RobotTest.Robot( new Location(new Point(3, 2), Direction.North));
            var robotLocation = commandProcessorRobotWorld.ExecuteCommands("FRRFLLFFRRFLL", robot);

            var robot2 = new RobotTest.Robot( new Location(new Point(3, 2), Direction.North));
            var robotLocation2 = commandProcessorRobotWorld.ExecuteCommands("FRRFLLFFRRFLL", robot2);

            Assert.AreEqual("3 3 North LOST", robotLocation);
            Assert.IsFalse(robotLocation2.Contains("LOST"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "Co-oords cannot be larger than 50")]
        public void The_maximum_value_for_any_coordinate_is_50()
        {
            new Point(51, 3);          
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException), ExpectedMessage = "Commands cannot be larger than 100")]
        public void All_instruction_strings_will_be_less_than_100_characters_in_length()
        {
            var commandInterpreter = new CommandInterpreter(new List<Command>());
            commandInterpreter.Interpret(new string('L', 100));            
        }      
  
   }
}
