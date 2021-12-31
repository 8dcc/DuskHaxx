using System;
using UnityEngine;

namespace DuskHaxx
{
    class CommandProcessor : MonoBehaviour
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
                case "tp":
                    ConsoleCommands.TeleportPlayer(new Vector3(float.Parse(command_array[1]), float.Parse(command_array[2]), float.Parse(command_array[3])));
                    break;
                case "kill":
                    ConsoleCommands.SetPlayerHealth(0f);
                    break;
                case "set_health":
                    ConsoleCommands.SetPlayerHealth(float.Parse(command_array[1]));
                    break;
                case "set_noclip_speed":
                    ConsoleCommands.SetNoclipSpeed(float.Parse(command_array[1]));
                    break;
                default:
                    break;
            }
        }
    }
}
