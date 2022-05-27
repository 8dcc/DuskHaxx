using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx
{
    class Aimbot : MonoBehaviour
    {
        class LocalVariables
        {
            public static List<Vector3> enemyPos = new List<Vector3>();
            public static Vector2 screen_center = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));

            public static Quaternion old_mouselook_rotation, old_player_rotation;
            public static bool aux_using_aimbot = false;
        }

        public void Update()
        {
            if (variables.CheatState.aimbot_bool)
            {
                LocalVariables.enemyPos.Clear();

                foreach (BasicAIScript enemy in UnityEngine.Object.FindObjectsOfType(typeof(BasicAIScript)) as BasicAIScript[])
                {
                    // Apply offset
                    Vector3 niggaballs = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.3f, enemy.transform.position.z);
                    LocalVariables.enemyPos.Add(niggaballs);
                }
            }
        }

        public void OnGUI()
        {
            if (variables.CheatState.aimbot_bool &&
                    !variables.Menu.menu_open &&
                    (Input.GetKey(variables.CheatSettings.aimbot_key1) || Input.GetKey(variables.CheatSettings.aimbot_key2)) &&
                    LocalVariables.enemyPos.Count > 0)
            {
                // Store rotation before aimbot if the setting is enabed
                if (!LocalVariables.aux_using_aimbot && variables.CheatSettings.restore_rotation_after_aimbot)
                {
                    MyMouseLook myMouseLook_old = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                    LocalVariables.old_mouselook_rotation = myMouseLook_old.transform.rotation;

                    LocalVariables.old_player_rotation = ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.rotation;
                }

                // Check if target is in fov and get the "closest"
                Vector3 best_target = LocalVariables.enemyPos[0];
                foreach (Vector3 enemy in LocalVariables.enemyPos)
                {
                    float pos_dif = Vector3.Distance(FindObjectOfType<Camera>().transform.position, enemy);
                    float pos_dif_bt = Vector3.Distance(FindObjectOfType<Camera>().transform.position, best_target);
                    if ( pos_dif + variables.CheatSettings.aimbot_closest_margin < pos_dif_bt && EnemyInFOV(GameObject.Find("MainCamera"), enemy) )
                        best_target = enemy;
                }

                // Actual aimbot
                if (best_target != new Vector3(0, 0, 0))
                {
                    GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = false;  // Disable player control

                    // Look at target
                    MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                    myMouseLook.transform.LookAt(best_target);

                    ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.LookAt(best_target);

                    LocalVariables.aux_using_aimbot = true;
                }            
            }
            else if (LocalVariables.aux_using_aimbot)   // After we release alt
            {
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = true;       // Enable player control

                // Restore old rotation if the setting is enabled
                if (variables.CheatSettings.restore_rotation_after_aimbot)
                {
                    MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                    myMouseLook.transform.rotation = LocalVariables.old_mouselook_rotation;

                    ((MyControllerScript[])UnityEngine.Object.FindObjectsOfType(typeof(MyControllerScript)))[0].transform.rotation = LocalVariables.old_player_rotation;
                }

                LocalVariables.aux_using_aimbot = false;
            }
        }

        public static bool EnemyInFOV(GameObject looker, Vector3 enemy_pos)
        {
            Vector3 targetDir = enemy_pos - GameObject.Find("MainCamera").transform.position;
            float angle = Vector3.Angle(targetDir, looker.transform.forward);

            return (angle < variables.CheatSettings.aimbot_fov);
        }
    }
}
