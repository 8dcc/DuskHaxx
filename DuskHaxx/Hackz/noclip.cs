using System;
using UnityEngine;

namespace DuskHaxx
{

    // https://github.com/TheReal3rd/CometVTwo/blob/master/CometVTwo/Modules/Hacks/InGame/Movement/NoClip.cs Modified it a bit
    public class NoClip : MonoBehaviour
    {
        private static Vector3 playerPos;
        
        class LocalVariables
        {
            public static bool old_restore_rotation, aux_first_time = true;
            public static bool noclip_enabled_with_key = false;
        }

        public void Update()
        {
            if (variables.CheatState.noclip_bool && Input.GetKeyDown(variables.CheatSettings.noclip_key))
            {
                LocalVariables.noclip_enabled_with_key = !LocalVariables.noclip_enabled_with_key;
            }

            if (LocalVariables.noclip_enabled_with_key)
            {
                // If you use aimbot and noclip, restore previous rotations to prevent buggy stuff
                if (LocalVariables.aux_first_time)
                {
                    LocalVariables.old_restore_rotation = variables.CheatSettings.restore_rotation_after_aimbot;
                    if (!variables.CheatSettings.restore_rotation_after_aimbot)
                    {
                        variables.CheatSettings.restore_rotation_after_aimbot = true;
                    }
                    playerPos = ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position;
                    LocalVariables.aux_first_time = false;
                }

                MyControllerScript script = ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0];
                if (Input.GetKey(KeyCode.W))  // Forward
                {
                    Vector3 forward = script.transform.forward;
                    playerPos += new Vector3((forward.x / 10) * variables.CheatSettings.noclip_speed, forward.y, 
                        (forward.z / 10) * variables.CheatSettings.noclip_speed);
                }
                if (Input.GetKey(KeyCode.S))  // Backwards
                {
                    Vector3 forward = script.transform.forward;
                    playerPos -= new Vector3((forward.x / 10) * variables.CheatSettings.noclip_speed, forward.y, 
                        (forward.z / 10) * variables.CheatSettings.noclip_speed);
                }
                if (Input.GetKey(KeyCode.A))  // Left
                {
                    Vector3 right = script.transform.right;
                    playerPos -= new Vector3((right.x / 10) * variables.CheatSettings.noclip_speed, right.y, 
                        (right.z / 10) * variables.CheatSettings.noclip_speed);
                }
                if (Input.GetKey(KeyCode.D))  // Right
                {
                    Vector3 right = script.transform.right;
                    playerPos += new Vector3((right.x / 10) * variables.CheatSettings.noclip_speed, right.y, 
                        (right.z / 10) * variables.CheatSettings.noclip_speed);
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    Vector3 up = script.transform.up;
                    playerPos += new Vector3(up.x, (up.y / 10) * variables.CheatSettings.noclip_speed, up.z);
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Vector3 up = script.transform.up;
                    playerPos -= new Vector3(up.x, (up.y / 10) * variables.CheatSettings.noclip_speed, up.z);
                }
                ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.position = playerPos;
            }
            else if (!LocalVariables.aux_first_time) {  // Next frame after we turn off noclip
                variables.CheatSettings.restore_rotation_after_aimbot = LocalVariables.old_restore_rotation;
                LocalVariables.aux_first_time = true;
            }

        }

        public static Vector3 PlayerPos
        {
            set => playerPos = value;
        }
    }
}