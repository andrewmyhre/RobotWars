namespace RobotWars.App
{
    public interface IRobotCommandMarshaller
    {
        void ExecuteInstructions(string instructions);
        void SetRobot(IRobot robot);
    }
}