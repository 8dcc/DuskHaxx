using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuskHaxx
{
    class CommandProcessor
    {
        public static void ParseCommand(string command)
        {
            string[] command_array = command.Split(' ');
            string command_name = command_array[0];

            switch (command_name)
            {
                case "set_fov":
                    ConsoleCommands.ChangeFov(float.Parse(command_array[1]));
                    break;
                case "set_timescale":
                    ConsoleCommands.ChangeTimescale(float.Parse(command_array[1]));
                    break;
                default:
                    break;
            }
        }
    }
}
