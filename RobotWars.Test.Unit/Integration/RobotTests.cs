using NUnit.Framework;
using RobotWars.App;

namespace RobotWars.Test.Integration
{
    [TestFixture]
    [Category("Integration")]
    public class RobotTests
    {
        [TestCase("mrm", 1, 1, OrientationEnum.East)]
        [TestCase("mmmmrmmmrmm", 3, 2, OrientationEnum.South)]
        [TestCase("mmrmmrmmrmmr", 0, 0, OrientationEnum.North)]
        [TestCase("rmmmlmmlmmlmmlmm", 3, 0, OrientationEnum.East)]
        [TestCase("mmmmrmmmrmmlmrrmmm", 1, 2, OrientationEnum.West)]
        public void ExecuteInstructions_RobotEndsUpWhereItShould(string instructions,
            int expectedX, int expectedY, OrientationEnum expectedOrientation)
        {
            // arrange
            IGrid grid = new Grid(10,10);
            IRobotCommandMarshaller commandMarshaller = new RobotCommandMarshaller();
            IRobot robot = new Robot(0,0,OrientationEnum.North, grid, commandMarshaller);

            // act
            robot.ExecuteInstructions(instructions);

            // assert
            Assert.That(robot.X, Is.EqualTo(expectedX));
            Assert.That(robot.Y, Is.EqualTo(expectedY));
            Assert.That(robot.Orientation, Is.EqualTo(expectedOrientation));
        }
    }
}
