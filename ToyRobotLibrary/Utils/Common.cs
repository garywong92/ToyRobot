using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotLibrary
{
    public class Common
    {
        public enum Command
        {
            PLACE,
            MOVE,
            LEFT,
            RIGHT,
            REPORT
        }

        public enum Orientation
        {
            NORTH,
            SOUTH,
            EAST,
            WEST
        }
    }
}
