using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToyRobotLibrary;

namespace ToyRobotApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var _toyRobot = new ToyRobot();

                // display welcome message
                string welcomeMessage = _toyRobot.GetWelcomeMessage();
                Console.WriteLine(welcomeMessage);

                while (true)
                {
                    // read user input
                    string userCommand = Console.ReadLine();

                    // send user command to robot
                    string responseMessage = _toyRobot.PerformCommand(userCommand);
                    if (responseMessage.Length > 0)
                    {
                        Console.WriteLine(responseMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // todo: log error
            }
        }
    }
}
