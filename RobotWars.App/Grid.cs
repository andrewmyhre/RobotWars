using System;
using System.Collections.Generic;

namespace RobotWars.App
{
    public class Grid : List<IRobot>, IGrid
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Grid(string initialiseString)
        {
            string[] parts = initialiseString.Split(' ');
            if (parts.Length != 2)
                throw new ArgumentException("Setup string must be in the format [Width] [Height] separated by a space e.g: 10 10"); 

            int tryWidth, tryHeight;
            if (!int.TryParse(parts[0], out tryWidth) || tryWidth < 0)
                throw new ArgumentException(string.Format("{0} is not a valid value for Width", parts[0]));
            if (!int.TryParse(parts[1], out tryHeight) || tryHeight < 0)
                throw new ArgumentException(string.Format("{0} is not a valid value for Height", parts[1]));

            Width = tryWidth;
            Height = tryHeight;
        }
    }
}