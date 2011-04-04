using System;
using NUnit.Framework;
using RobotWars.App;

namespace RobotWars.Test.Unit
{
    [TestFixture]
    [Category("Unit")]
    public class GridTests
    {
        [TestCase("1 1", 1, 1)]
        [TestCase("1 5", 1, 5)]
        [TestCase("5 5", 5, 5)]
        [TestCase("50 50", 50, 50)]
        public void Ctor_GivenAValidSetupString_GridIsInitialisedWithCorrectValues(string initialiseString, int expectedWidth, int expectedHeight)
        {
            // act
            var grid = new Grid(initialiseString);

            // assert
            Assert.That(grid.Width, Is.EqualTo(expectedWidth));
            Assert.That(grid.Height, Is.EqualTo(expectedHeight));
        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("a b")]
        [TestCase("-1 -1")]
        [TestCase("-1 5")]
        [TestCase("5 -5")]
        [TestCase("- 50")]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_GivenAnInvalidSetupString_ThrowsArgumentException(string initialiseString)
        {
            Console.WriteLine(initialiseString);

            // act
            new Grid(initialiseString);
        }
    }
}
