using System;
using UnityEngine;

namespace DuskHaxx
{
    class DebugPlayerFov : MonoBehaviour
    {
        class LocalVariables
        {
            public static string player_fov_string = "";
            public static float fov_offset_round = 0.74f;
            public static float fov_offset = 31f;
        }

        public void OnGUI()
        {
            if (variables.CheatState.player_fov_bool)
            {
                float slider_fov = ((GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript))).LoadConfigFloat("fov");
                if (ConsoleCommands.custom_fov == 0f)
                {
                    if (variables.CheatSettings.round_fov)
                    {
                        float slider_mult = slider_fov * LocalVariables.fov_offset_round;
                        float rounded_value = (float)(Math.Round((double)slider_mult, 0));
                        FindObjectOfType<Camera>().fieldOfView = rounded_value; // -25? -31?
                    } else FindObjectOfType<Camera>().fieldOfView = slider_fov - LocalVariables.fov_offset; // -25? -31?
                } else FindObjectOfType<Camera>().fieldOfView = ConsoleCommands.custom_fov;

                float player_fov_info = FindObjectOfType<Camera>().fieldOfView;

                LocalVariables.player_fov_string = "Fov: ";
                LocalVariables.player_fov_string += slider_fov.ToString("0");
                LocalVariables.player_fov_string += "/";
                LocalVariables.player_fov_string += player_fov_info.ToString("0");
                LocalVariables.player_fov_string += " ";

                NullsRenderer.DrawWatermark(new Vector2(270f, 5f), LocalVariables.player_fov_string, true);
            }
        }
    }
}
