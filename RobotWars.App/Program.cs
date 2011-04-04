using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotWars.App
{
    class Program
    {
        private static IGrid _grid = null;
        private static List<IRobot> _robots = null;
        static void Main(string[] args)
        {
            Exception e = null;

            // initialise a grid
            do
            {
                try
                {
                    Console.Write("Grid size (Width Height): ");
                    _grid = new Grid(Console.ReadLine());
                    e = null;
                } catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    e = exception;
                }
            } while (e != null);

            List<IRobot> robots = new List<IRobot>();
            
            // set up two robots
            Console.Write("Robot 1 initialisation (X Y [N|E|S|W}): ");
            robots.Add(InitialiseRobot());
            Console.Write("Robot 2 initialisation (X Y [N|E|S|W}): ");
            robots.Add(InitialiseRobot());

            do
            {
                for (int i = 0; i < robots.Count;i++ )
                {
                    Console.Write(string.Format("Robot {0} instructions: ", (i+1)));
                    ExecuteTurn(robots[i]);
                }
            } while (true);  

#if DEBUG
            Console.WriteLine("Finished, press any key to continue.");
            Console.ReadKey();
#endif
        }

        private static IRobot InitialiseRobot()
        {
            IRobot robot=null;
            Exception e;
            do
            {
                try
                {
                    robot = new Robot(Console.ReadLine(), _grid);
                    e = null;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    e = exception;
                }
            } while (e != null);
            return robot;
        }

        private static void ExecuteTurn(IRobot robot)
        {
            Exception e;
            do
            {
                try
                {
                    robot.ExecuteInstructions(Console.ReadLine());
                    e = null;
                } catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    e = exception;
                }
                    
            } while (e != null);
            Console.WriteLine("{0} {1} {2}", robot.X, robot.Y, Enum.GetName(typeof(OrientationEnum), robot.Orientation).Substring(0,1));
        }
    }
}
