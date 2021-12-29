using System;
using UnityEngine;

namespace DuskHaxx
{
    class DebugPlayerPos : MonoBehaviour
    {
        class LocalVariables
        {
            public static string player_pos_string = "";
        }

        public void OnGUI()
        {
            if (variables.CheatState.player_pos_bool)
            {
                var player_pos_info = FindObjectOfType<Camera>().transform.position;
                //var player_pos_info = GameObject.Find("MainCamera").transform.position;

                LocalVariables.player_pos_string = "Pos: ";
                LocalVariables.player_pos_string += player_pos_info.ToString("F0");

                NullsRenderer.DrawWatermark(new Vector2(120f, 5f), LocalVariables.player_pos_string, true);
            } // player_pos_bool is disabled
        }
    }
}
