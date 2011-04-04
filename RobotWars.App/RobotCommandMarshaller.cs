using System;

namespace RobotWars.App
{
    public class RobotCommandMarshaller : IRobotCommandMarshaller
    {
        private IRobot _robot;

        public RobotCommandMarshaller(IRobot robot)
        {
            _robot = robot;
        }

        public RobotCommandMarshaller()
        {
        }

        public void ExecuteInstructions(string instructions)
        {
            instructions = instructions.ToLower();
            foreach (char i in instructions)
            {
                switch (i)
                {
                    case 'l':
                        _robot.CommandLeft();
                        break;
                    case 'r':
                        _robot.CommandRight();
                        break;
                    case 'm':
                        _robot.CommandMove();
                        break;
                    default: throw new ArgumentException("Invalid instruction " + i);
                }
            }

        }

        public void SetRobot(IRobot robot)
        {
            _robot = robot;
        }
    }
}