using System;
using Moq;
using NUnit.Framework;
using RobotWars.App;

namespace RobotWars.Test.Unit
{
    [TestFixture]
    [Category("Unit")]
    public class RobotCommandMarshallerTests
    {
        [TestCase("a")]
        [TestCase("b")]
        [TestCase("c")]
        [TestCase("d")]
        [TestCase("!")]
        [TestCase("4")]
        [TestCase("$")]
        [TestCase("lra")]
        [TestCase("mm5")]
        [TestCase("9am")]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCommand_ThrowsException(string invalidInstruction)
        {
            // arrange
            var commandMarshaller = new RobotCommandMarshaller(new Mock<IRobot>().Object);

            // act
            commandMarshaller.ExecuteInstructions(invalidInstruction);
        }

        [Test]
        public void ParseInstructions_GivenASequenceOfInstructions_ParsesEachInstructionInSequence()
        {
            // arrange
            string instructions = "llmmmrmm";
            var robot = new Mock<IRobot>();
            robot
                .Setup(r=>r.CommandLeft())
                .Verifiable();
            robot
                .Setup(r => r.CommandRight())
                .Verifiable();
            robot
                .Setup(r => r.CommandMove())
                .Verifiable();
            var commandMarshaller = new RobotCommandMarshaller(robot.Object);

            // act
            commandMarshaller.ExecuteInstructions(instructions);

            // assert
            robot.Verify(r=>r.CommandLeft(), Times.Exactly(2));
            robot.Verify(r => r.CommandRight(), Times.Exactly(1));
            robot.Verify(r => r.CommandMove(), Times.Exactly(5));
        }

        [Test]
        public void ParseInstructionLeft_InvokesCommandLeft()
        {
            // arrange
            string instructions = "L";
            Mock<IRobot> robot = new Mock<IRobot>();
            robot
                .Setup(r => r.CommandLeft())
                .Verifiable();
            IRobotCommandMarshaller commandMarshaller = new RobotCommandMarshaller(robot.Object);

            // act
            commandMarshaller.ExecuteInstructions(instructions);

            // assert
            robot.Verify(r => r.CommandLeft(), Times.Once());

        }
        [Test]
        public void ParseInstructionRight_InvokesCommandRight()
        {
            // arrange
            string instructions = "R";
            Mock<IRobot> robot = new Mock<IRobot>();
            robot
                .Setup(r => r.CommandRight())
                .Verifiable();
            IRobotCommandMarshaller commandMarshaller = new RobotCommandMarshaller(robot.Object);

            // act
            commandMarshaller.ExecuteInstructions(instructions);

            // assert
            robot.Verify(r => r.CommandRight(), Times.Once());
        }
        [Test]
        public void ParseInstructionMove_InvokesCommandMove()
        {
            // arrange
            string instructions = "M";
            Mock<IRobot> robot = new Mock<IRobot>();
            robot
                .Setup(r => r.CommandMove())
                .Verifiable();
            IRobotCommandMarshaller commandMarshaller = new RobotCommandMarshaller(robot.Object);

            // act
            commandMarshaller.ExecuteInstructions(instructions);

            // assert
            robot.Verify(r => r.CommandMove(), Times.Once());
        }
    }
}
