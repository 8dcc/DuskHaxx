using System;
using UnityEngine;

namespace DuskHaxx
{
    class DebugPlayerFov : MonoBehaviour
    {
        class LocalVariables
        {
            public static string player_fov_string = "";
        }

        public void OnGUI()
        {
            if (variables.CheatState.player_fov_bool)
            {
                FindObjectOfType<Camera>().fieldOfView = ((GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript))).LoadConfigFloat("fov") - 25;

                //var player_fov_info = GameObject.Find("MainCamera").transform.position;
                //float player_fov_info = ((GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript))).LoadConfigFloat("fov");
                float player_fov_info = FindObjectOfType<Camera>().fieldOfView;

                LocalVariables.player_fov_string = "Fov: ";
                LocalVariables.player_fov_string += player_fov_info.ToString("0");
                LocalVariables.player_fov_string += " ";

                NullsRenderer.DrawWatermark(new Vector2(270f, 5f), LocalVariables.player_fov_string, true);
            }
        }
    }
}
