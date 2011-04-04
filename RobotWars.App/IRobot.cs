namespace RobotWars.App
{
    public interface IRobot
    {
        int X { get;  }
        int Y { get;  }
        OrientationEnum Orientation { get; }
        void CommandLeft();
        void CommandRight();
        void CommandMove();
        void ExecuteInstructions(string instructions);
    }
}