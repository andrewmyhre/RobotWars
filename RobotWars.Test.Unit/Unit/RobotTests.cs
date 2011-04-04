using System;
using System.Collections.Generic;
using NUnit.Framework;
using RobotWars.App;

namespace RobotWars.Test.Unit
{
    [TestFixture]
    [Category("Unit")]
    public class RobotTests
    {
        private IGrid fiveByFiveGrid = null;
        [SetUp]
        public void Setup()
        {
            fiveByFiveGrid = new Grid(5,5);
        }


        [TestCase(OrientationEnum.North, OrientationEnum.West)]
        [TestCase(OrientationEnum.West, OrientationEnum.South)]
        [TestCase(OrientationEnum.South, OrientationEnum.East)]
        [TestCase(OrientationEnum.East, OrientationEnum.North)]
        public void CommandLeft_ResultsInCorrectOrientation(OrientationEnum start, OrientationEnum expectedResult)
        {
            // arrange
            IRobot robot = new Robot(0, 0, start, fiveByFiveGrid);

            // act
            robot.CommandLeft();

            // assert
            Assert.That(robot.Orientation, Is.EqualTo(expectedResult), 
                string.Format("left rotation from {0} to {1} failed", start, expectedResult));
            Console.WriteLine(string.Format("left rotation from {0} to {1} succeeded", start, expectedResult));
        }

        [TestCase(OrientationEnum.North, OrientationEnum.East)]
        [TestCase(OrientationEnum.East, OrientationEnum.South)]
        [TestCase(OrientationEnum.South, OrientationEnum.West)]
        [TestCase(OrientationEnum.West, OrientationEnum.North)]
        public void CommandRight_ResultsInCorrectOrientation(OrientationEnum start, OrientationEnum expectedResult)
        {
            // arrange
            IRobot robot = new Robot(0, 0, start, fiveByFiveGrid);

            // act
            robot.CommandRight();

            // assert
            Assert.That(robot.Orientation, Is.EqualTo(expectedResult),
                string.Format("right rotation from {0} to {1} failed", start, expectedResult));
            Console.WriteLine(string.Format("right rotation from {0} to {1} succeeded", start, expectedResult));
        }

        [Test]
        public void CommandMove_FacingNorth_IncrementY()
        {
            // arrange
            IRobot robot = new Robot(0, 0, OrientationEnum.North, fiveByFiveGrid);

            // act
            robot.CommandMove();

            // assert
            Assert.That(robot.Y, Is.EqualTo(1));
            Assert.That(robot.X, Is.EqualTo(0));
        }
        [Test]
        public void CommandMove_FacingEast_IncrementX()
        {
            // arrange
            IRobot robot = new Robot(0, 0, OrientationEnum.East, fiveByFiveGrid);

            // act
            robot.CommandMove();

            // assert
            Assert.That(robot.X, Is.EqualTo(1));
            Assert.That(robot.Y, Is.EqualTo(0));
        }
        [Test]
        public void CommandMove_FacingSouth_DecrementY()
        {
            // arrange
            IRobot robot = new Robot(1, 1, OrientationEnum.South, fiveByFiveGrid);

            // act
            robot.CommandMove();

            // assert
            Assert.That(robot.X, Is.EqualTo(1));
            Assert.That(robot.Y, Is.EqualTo(0));
        }
        [Test]
        public void CommandMove_FacingWest_DecrementX()
        {
            // arrange
            IRobot robot = new Robot(1, 1, OrientationEnum.West, fiveByFiveGrid);

            // act
            robot.CommandMove();

            // assert
            Assert.That(robot.X, Is.EqualTo(0));
            Assert.That(robot.Y, Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandMove_FacingNorthAndAtTopOfGrid_ThrowsException()
        {
            // arrange
            IGrid grid = new Grid(5,5);
            IRobot robot = new Robot(0, 4, OrientationEnum.North, fiveByFiveGrid);
            grid.Add(robot);

            // act
            robot.CommandMove();
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandMove_FacingEastAndAtFarRightOfGrid_ThrowsException()
        {
            // arrange
            IGrid grid = new Grid(5, 5);
            IRobot robot = new Robot(4, 0, OrientationEnum.East, grid);
            grid.Add(robot);

            // act
            robot.CommandMove();
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandMove_FacingSouthAndAtBottomOfGrid_ThrowsException()
        {
            // arrange
            IGrid grid = new Grid(5, 5);
            IRobot robot = new Robot(0, 0, OrientationEnum.South, grid);
            grid.Add(robot);

            // act
            robot.CommandMove();

        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandMove_FacingWestAndAtFarLeftOfGrid_ThrowsException()
        {
            // arrange
            IGrid grid = new Grid(5, 5);
            IRobot robot = new Robot(0, 0, OrientationEnum.West, grid);
            grid.Add(robot);

            // act
            robot.CommandMove();

        }

        [TestCase("1 1 N", 1, 1, OrientationEnum.North)]
        [TestCase("4 2 E", 4, 2, OrientationEnum.East)]
        [TestCase("2 3 S", 2, 3, OrientationEnum.South)]
        [TestCase("3 4 W", 3, 4, OrientationEnum.West)]
        public void Ctor_CanInitialiseFromString(string setupString, int expectedX, int expectedY, OrientationEnum expectedOrientation)
        {
            // act
            IRobot robot = new Robot(setupString, fiveByFiveGrid);

            //Assert
            Assert.That(robot.X, Is.EqualTo(expectedX));
            Assert.That(robot.Y, Is.EqualTo(expectedY));
            Assert.That(robot.Orientation, Is.EqualTo(expectedOrientation));
        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("1 2")]
        [TestCase("3 N 1")]
        [TestCase("1 2 O")]
        [TestCase("4 4 T")]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_InvalidSetupString_ThrowsArgumentException(string setupString)
        {
            // act
            Console.WriteLine(setupString);
            new Robot(setupString, fiveByFiveGrid);
        }

        [TestCase("-1 -1 N")]
        [TestCase("6 4 N")]
        [TestCase("2 6 N")]
        [TestCase("10 10 N")]
        [TestCase("5 5 N")] // grid is zero based so this should throw
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_SetupStringPlacesRobotOutsideGrid_ThrowsArgumentException(string setupString)
        {
            Console.WriteLine(setupString);
            
            // act
            new Robot(setupString, fiveByFiveGrid);
        }


        /* 
         * AM; this is a terser way to express the CommandLeft_ and CommandRight_ tests
         * it's quite clever but it's not very readable
         * would be better if you could provide a delegate as a TestCase argument directly
         * */
        [TestCaseSource("CommandTestCases")]
        public void Command_ResultsInCorrectOrientation(CommandTestCaseData testCase)
        {
            // arrange
            IRobot robot = new Robot(0, 0, testCase.StartOrientation, fiveByFiveGrid);

            // act
            testCase.Command.Invoke(robot);

            // assert
            Assert.That(robot.Orientation, Is.EqualTo(testCase.ExpectedOrientation));
        }

        public class CommandTestCaseData
        {
            public CommandTestCaseData(OrientationEnum startOrientation, Action<IRobot> command, OrientationEnum expectedResult)
            {
                StartOrientation = startOrientation;
                Command = command;
                ExpectedOrientation = expectedResult;
            }

            public Action<IRobot> Command { get; set; }
            public OrientationEnum StartOrientation { get; set; }
            public OrientationEnum ExpectedOrientation { get; set; }
        }

        public IEnumerable<CommandTestCaseData> CommandTestCases()
        {
            yield return new CommandTestCaseData(OrientationEnum.North, r => r.CommandLeft(), OrientationEnum.West);
            yield return new CommandTestCaseData(OrientationEnum.West, r => r.CommandLeft(), OrientationEnum.South);
            yield return new CommandTestCaseData(OrientationEnum.South, r => r.CommandLeft(), OrientationEnum.East);
            yield return new CommandTestCaseData(OrientationEnum.East, r => r.CommandLeft(), OrientationEnum.North);
            yield return new CommandTestCaseData(OrientationEnum.North, r => r.CommandRight(), OrientationEnum.East);
            yield return new CommandTestCaseData(OrientationEnum.East, r => r.CommandRight(), OrientationEnum.South);
            yield return new CommandTestCaseData(OrientationEnum.South, r => r.CommandRight(), OrientationEnum.West);
            yield return new CommandTestCaseData(OrientationEnum.West, r => r.CommandRight(), OrientationEnum.North);
        }
    }
}
