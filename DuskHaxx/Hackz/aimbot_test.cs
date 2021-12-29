using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx
{
    class Aimbot : MonoBehaviour
    {
        class LocalVariables
        {
            public static int aimbot_fov = 40;
            public static Quaternion targetRotation;
            public static Quaternion lookAt;

            //public static float old_xrotation, old_yrotation;
            public static Quaternion old_maincamera_rotation;
            public static Quaternion old_mouselook_rotation;

            public static List<Vector3> enemyPos = new List<Vector3>();
            public static bool aux_using_aimbot = false;
        }

        public void Update()
        {
            if (variables.CheatState.aimbot_bool)
            {
                LocalVariables.enemyPos.Clear();

                foreach (EnemyIndicatorObjectScript enemy in UnityEngine.Object.FindObjectsOfType(typeof(EnemyIndicatorObjectScript)) as EnemyIndicatorObjectScript[])
                {
                    // Apply and offset but its kinda trash if you change the fov
                    Vector3 anime_feet;
                    anime_feet.x = enemy.transform.position.x;
                    anime_feet.y = enemy.transform.position.y - 1f;
                    anime_feet.z = enemy.transform.position.z;

                    LocalVariables.enemyPos.Add(anime_feet);
                }
            }
        }

        public void OnGUI()
        {
            if (variables.CheatState.aimbot_bool && Input.GetKey(KeyCode.Mouse1) && !variables.Menu.menu_open && LocalVariables.enemyPos.Count > 0)
            {
                // Store rotation before aimbot
                if (!LocalVariables.aux_using_aimbot)
                {
                    MyMouseLook myMouseLook_old = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));

                    LocalVariables.old_maincamera_rotation = GameObject.Find("MainCamera").transform.rotation;
                    LocalVariables.old_mouselook_rotation = myMouseLook_old.transform.rotation;
                }

                Vector3 best_target = new Vector3(0,0,0);
                foreach (Vector3 enemy in LocalVariables.enemyPos)
                {
                    Vector3 pos_dif = enemy - FindObjectOfType<Camera>().transform.position;
                    Vector3 pos_dif_bt = best_target - FindObjectOfType<Camera>().transform.position;
                    if (EnemyInFOV(GameObject.Find("MainCamera"), enemy) && (pos_dif.x < pos_dif_bt.x || pos_dif.z < pos_dif_bt.z))
                    {
                        best_target = enemy;
                    }
                }

                if (best_target != new Vector3(0, 0, 0))
                {
                    GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = false; // Disable player control

                    Vector3 direction = best_target - GameObject.Find("MainCamera").transform.position;
                    LocalVariables.targetRotation = Quaternion.LookRotation(direction);
                    LocalVariables.lookAt = Quaternion.RotateTowards(GameObject.Find("MainCamera").transform.rotation, LocalVariables.targetRotation, Time.deltaTime * 750);
                    GameObject.Find("MainCamera").transform.rotation = LocalVariables.lookAt;

                    LocalVariables.aux_using_aimbot = true;
                }
            // After we release alt
            } else if (LocalVariables.aux_using_aimbot) {
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = true; // Enable player control

                // Restore old rotation
                GameObject.Find("MainCamera").transform.rotation = LocalVariables.old_maincamera_rotation;

                MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                myMouseLook.transform.rotation = LocalVariables.old_mouselook_rotation;

                LocalVariables.aux_using_aimbot = false;
            }
        }

        bool EnemyInFOV(GameObject looker, Vector3 enemy_pos)
        {
            Vector3 targetDir = enemy_pos - GameObject.Find("MainCamera").transform.position;
            float angle = Vector3.Angle(targetDir, looker.transform.forward);

            return (angle < LocalVariables.aimbot_fov);
        }
    }
}
