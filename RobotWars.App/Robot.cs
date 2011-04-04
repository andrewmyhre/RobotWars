using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotWars.App
{
    public class Robot : IRobot
    {
        private readonly IRobotCommandMarshaller _commandMarshaller;
        private readonly IGrid _grid;
        public int X { get; set; }
        public int Y { get; set; }
        public OrientationEnum Orientation { get; set; }

        public Robot(int initialX, int initialY, OrientationEnum orientation, IGrid grid) 
            : this(initialX, initialY, orientation, grid, new RobotCommandMarshaller()){}

        public Robot(int initialX, int initialY, OrientationEnum orientation, IGrid grid, IRobotCommandMarshaller commandMarshaller)
        {
            _commandMarshaller = commandMarshaller;
            _commandMarshaller.SetRobot(this);

            _grid = grid;
            X = initialX;
            Y = initialY;
            Orientation = orientation;
        }

        public Robot(string setupString, IGrid grid) : this(setupString, grid, new RobotCommandMarshaller()){}
        public Robot(string setupString, IGrid grid, IRobotCommandMarshaller commandMarshaller)
        {
            _commandMarshaller = commandMarshaller;
            commandMarshaller.SetRobot(this);

            _grid = grid;
            string[] setupParts = setupString.Split(' ');
            if (setupParts.Length != 3) 
                throw new ArgumentException("To set up a robot provide X Y [N|E|S|W] values seperated by spaces\ne.g: 1 1 N");

            int tryX = 0, tryY = 0;
            if (!int.TryParse(setupParts[0], out tryX))
                throw new ArgumentException(string.Format("{0} is not a valid value for X", setupParts[0]), "setupString");
            if (!int.TryParse(setupParts[1], out tryY))
                throw new ArgumentException(string.Format("{0} is not a valid value for Y", setupParts[1]), "setupString");
            if (tryX < 0 || tryX >= grid.Width || tryY < 0 || tryY >= grid.Height)
                throw new ArgumentException(string.Format("The coordinates {0},{1} are outside the grid", setupParts[0],
                                                          setupParts[1]), "setupString");

            X = tryX;
            Y = tryY;
            switch(setupParts[2])
            {
                case "N":
                    Orientation = OrientationEnum.North;
                    break;
                case "E":
                    Orientation = OrientationEnum.East;
                    break;
                case "S":
                    Orientation = OrientationEnum.South;
                    break;
                case "W":
                    Orientation = OrientationEnum.West;
                    break;
                default:
                    throw new ArgumentException(string.Format("{0} is not a valid value for Orientation", setupParts[2]), "setupString");
            }
        }

        public void CommandLeft()
        {
            --Orientation;
            if (!Enum.IsDefined(typeof(OrientationEnum), (int)Orientation))
                Orientation = Enum.GetValues(typeof(OrientationEnum)).Cast<OrientationEnum>().Last();
        }

        public void CommandRight()
        {
            ++Orientation;
            if (!Enum.IsDefined(typeof(OrientationEnum), (int)Orientation))
                Orientation = 0;
        }

        public void CommandMove()
        {
            switch(Orientation)
            {
                case OrientationEnum.North:
                    if (Y+1==_grid.Height) throw new InvalidOperationException("Cannot move outside the grid!");
                    ++Y;
                    break;
                case OrientationEnum.East:
                    if (X+1 == _grid.Width) throw new InvalidOperationException("Cannot move outside the grid!");
                    ++X;
                    break;
                case OrientationEnum.West:
                    if (X == 0) throw new InvalidOperationException("Cannot move outside the grid!");
                    --X;
                    break;
                case OrientationEnum.South:
                    if (Y == 0) throw new InvalidOperationException("Cannot move outside the grid!");
                    --Y;
                    break;
            }
        }

        public void ExecuteInstructions(string instructions)
        {
            _commandMarshaller.ExecuteInstructions(instructions);
        }
    }
}
