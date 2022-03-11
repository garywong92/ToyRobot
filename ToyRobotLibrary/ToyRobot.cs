using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotLibrary
{
    public class ToyRobot
    {
        public static AppSettingsReader reader = new AppSettingsReader();
        public static int tableMinSizeX = (int)reader.GetValue("ToyRobot.Table.MinPoint.X", typeof(int));
        public static int tableMinSizeY = (int)reader.GetValue("ToyRobot.Table.MinPoint.Y", typeof(int));
        public static int tableMaxSizeX = (int)reader.GetValue("ToyRobot.Table.MaxPoint.X", typeof(int));
        public static int tableMaxSizeY = (int)reader.GetValue("ToyRobot.Table.MaxPoint.Y", typeof(int));
        public static string welcomeMessage = (string)reader.GetValue("ToyRobot.WelcomeMessage", typeof(string));
        public static string errorMessage = (string)reader.GetValue("ToyRobot.InvalidCommand", typeof(string));
        public static string invalidCommand = (string)reader.GetValue("ToyRobot.InvalidCommand", typeof(string));
        public static string invalidMove = (string)reader.GetValue("ToyRobot.InvalidMove", typeof(string));

        public bool isPlaced = false;
        public int positionX = 0, positionY = 0;
        public string orientation = string.Empty;

        public string PerformCommand(string userCommand)
        {
            string responseMessage = string.Empty;

            try
            {
                // PLACE command
                if (userCommand.ToUpper().Contains(Common.Command.PLACE.ToString()))
                {
                    int cmdValueIndex = userCommand.ToUpper().IndexOf(Common.Command.PLACE.ToString()) + 5; // index position of command values
                    string cmdValue = userCommand.Substring(cmdValueIndex).Trim();
                    string[] cmdValueArray = cmdValue.Split(',');
                    bool validOrientationCmd = false;

                    // validate if command has both x and y values
                    if (cmdValueArray.Length >= 2)
                    {
                        string cmdValueX = string.Empty;
                        string cmdValueY = string.Empty;
                        string cmdValueOrientation = string.Empty;
                        int intCmdValueX = 0, intCmdValueY = 0;

                        cmdValueX = cmdValueArray[0].Trim();
                        cmdValueY = cmdValueArray[1].Trim();

                        // if command contains orientation param, validate command value
                        if (cmdValueArray.Length >= 3)
                        {
                            cmdValueOrientation = cmdValueArray[2].Trim().ToUpper();

                            if (cmdValueOrientation == Common.Orientation.NORTH.ToString() || cmdValueOrientation == Common.Orientation.SOUTH.ToString() ||
                                cmdValueOrientation == Common.Orientation.EAST.ToString() || cmdValueOrientation == Common.Orientation.WEST.ToString())
                            {
                                validOrientationCmd = true;
                            }
                        }
                        // if command does not contain orientation param
                        else
                        {
                            // PLACE command is allowed without orientation param when robot is already on table
                            if (isPlaced)
                            {
                                validOrientationCmd = true;
                            }
                            // PLACE command must have orientation param when placing robot on table for first time
                            else
                            {
                                validOrientationCmd = false;
                            }
                        }

                        // validate command values
                        if (int.TryParse(cmdValueX, out intCmdValueX) && int.TryParse(cmdValueY, out intCmdValueY) && validOrientationCmd)
                        {
                            // check if positions are within table size
                            if (intCmdValueX >= tableMinSizeX && intCmdValueX <= tableMaxSizeX && intCmdValueY >= tableMinSizeY && intCmdValueY <= tableMaxSizeY)
                            {
                                positionX = intCmdValueX;
                                positionY = intCmdValueY;

                                if (!string.IsNullOrEmpty(cmdValueOrientation))
                                {
                                    orientation = cmdValueOrientation;
                                }

                                isPlaced = true;

                                // PLACE command successful
                                return responseMessage;
                            }
                        }
                    }
                }
                // MOVE command
                else if (userCommand.ToUpper().Contains(Common.Command.MOVE.ToString()))
                {
                    // if robot is already on table, check if position ahead of robot is the edge of table, before moving forward by 1 unit
                    if (isPlaced)
                    {
                        if (orientation == Common.Orientation.NORTH.ToString())
                        {
                            if (positionY < tableMaxSizeY)
                            {
                                positionY++;

                                // MOVE command successful
                                return responseMessage;
                            }
                        }
                        else if (orientation == Common.Orientation.SOUTH.ToString())
                        {
                            if (positionY > tableMinSizeY)
                            {
                                positionY--;

                                // MOVE command successful
                                return responseMessage;
                            }
                        }
                        else if (orientation == Common.Orientation.EAST.ToString())
                        {
                            if (positionX < tableMaxSizeX)
                            {
                                positionX++;

                                // MOVE command successful
                                return responseMessage;
                            }
                        }
                        else if (orientation == Common.Orientation.WEST.ToString())
                        {
                            if (positionX > tableMinSizeX)
                            {
                                positionX--;

                                // MOVE command successful
                                return responseMessage;
                            }
                        }

                        return invalidMove;
                    }
                }
                // LEFT command
                else if (userCommand.ToUpper().Contains(Common.Command.LEFT.ToString()))
                {
                    // if robot is already on table, turn left 90 degrees from current orientation
                    if (isPlaced)
                    {
                        if (orientation == Common.Orientation.NORTH.ToString())
                        {
                            orientation = Common.Orientation.WEST.ToString();
                        }
                        else if (orientation == Common.Orientation.SOUTH.ToString())
                        {
                            orientation = Common.Orientation.EAST.ToString();
                        }
                        else if (orientation == Common.Orientation.EAST.ToString())
                        {
                            orientation = Common.Orientation.NORTH.ToString();
                        }
                        else if (orientation == Common.Orientation.WEST.ToString())
                        {
                            orientation = Common.Orientation.SOUTH.ToString();
                        }

                        // LEFT command successful
                        return responseMessage;
                    }
                }
                // RIGHT command
                else if (userCommand.ToUpper().Contains(Common.Command.RIGHT.ToString()))
                {
                    // if robot is already on table, turn right 90 degrees from current orientation
                    if (isPlaced)
                    {
                        if (orientation == Common.Orientation.NORTH.ToString())
                        {
                            orientation = Common.Orientation.EAST.ToString();
                        }
                        else if (orientation == Common.Orientation.SOUTH.ToString())
                        {
                            orientation = Common.Orientation.WEST.ToString();
                        }
                        else if (orientation == Common.Orientation.EAST.ToString())
                        {
                            orientation = Common.Orientation.SOUTH.ToString();
                        }
                        else if (orientation == Common.Orientation.WEST.ToString())
                        {
                            orientation = Common.Orientation.NORTH.ToString();
                        }

                        // RIGHT command successful
                        return responseMessage;
                    }
                }
                // REPORT command
                else if (userCommand.ToUpper().Contains(Common.Command.REPORT.ToString()))
                {
                    // if robot is already on table, report current position and orientation
                    if (isPlaced)
                    {
                        responseMessage = Report();

                        // REPORT command successful
                        return responseMessage;
                    }
                }

                responseMessage = invalidCommand;

                return responseMessage;
            }
            catch (Exception ex)
            {
                // todo: log error

                responseMessage = errorMessage;

                return responseMessage;
            }
        }

        public string GetWelcomeMessage()
        {
            return welcomeMessage;
        }

        public string Report()
        {
            return $"{positionX},{positionY},{orientation}";
        }
    }
}
