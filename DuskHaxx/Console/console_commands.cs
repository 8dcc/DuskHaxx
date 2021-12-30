using System;
using UnityEngine;

namespace DuskHaxx
{
    class ConsoleCommands : MonoBehaviour
    {
        public static float custom_fov = ((GameMenuButtonsScript)GameObject.Find("DasMenu").GetComponent(typeof(GameMenuButtonsScript))).LoadConfigFloat("fov") - 25;

        public static void ChangeFov(float fov)
        {
            custom_fov = fov;
        }

        public static void ChangeTimescale(float timescale)
        {
            Time.timeScale = timescale;
        }
    }
}
