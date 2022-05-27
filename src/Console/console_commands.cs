using System;
using UnityEngine;

namespace DuskHaxx
{
    class ConsoleCommands : MonoBehaviour
    {
        // 0f is the default state, if the debug_fov script detects that this value is 0, will change the fov automatically
        public static float custom_fov = 0f;  

        public static void ChangeFov(float fov)
        {
            custom_fov = fov;
        }

        public static void ChangeTimescale(float timescale)
        {
            Time.timeScale = timescale;
        }

        public static void TeleportPlayer(Vector3 new_position)
        {
            ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position = new_position;
        }

        public static void SetPlayerHealth(float health)
        {
            if (health == 0f && variables.CheatState.godmode_bool) variables.CheatState.godmode_bool = false;
            PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
            playerHealthManagement.myhealth = health;
        }

        public static void SetNoclipSpeed(float speed)
        {
            variables.CheatSettings.noclip_speed = speed;
        }

        public static void SetTracerDistance(float distance)
        {
            variables.CheatSettings.max_tracer_distance = distance;
        }
    }
}
