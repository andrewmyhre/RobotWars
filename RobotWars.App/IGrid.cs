using System.Collections.Generic;

namespace RobotWars.App
{
    public interface IGrid : IList<IRobot>
    {
        int Width { get; set; }
        int Height { get; set; }
    }
}