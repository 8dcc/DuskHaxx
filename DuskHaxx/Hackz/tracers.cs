using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx
{
    class Tracers : MonoBehaviour
    {
        class LocalVariables
        {
            public static Vector2 screen_center = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
            public static List<Vector3> enemyPos = new List<Vector3>();
            public static Color colour = Color.red;
        }

        public void Update()
        {
            if (variables.CheatState.tracer_bool)
            {
                LocalVariables.enemyPos.Clear();

                foreach (EnemyIndicatorObjectScript enemy in UnityEngine.Object.FindObjectsOfType(typeof(EnemyIndicatorObjectScript)) as EnemyIndicatorObjectScript[])
                {
                    // Apply and offset but its kinda trash if you change the fov
                    Vector3 anime_feet;
                    anime_feet.x = enemy.transform.position.x;
                    anime_feet.y = enemy.transform.position.y - 2.8f;
                    anime_feet.z = enemy.transform.position.z - 30f + Mathf.Sqrt(enemy.transform.position.x * enemy.transform.position.x + enemy.transform.position.y * enemy.transform.position.y);
                    //TODO THIS SHIT ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                    LocalVariables.enemyPos.Add(anime_feet);
                }
            }
        }

        public void OnGUI()
        {
            if (variables.CheatState.tracer_bool && LocalVariables.enemyPos.Count > 0)
            {
                var camera = FindObjectOfType<Camera>();

                foreach (var target in LocalVariables.enemyPos)
                {
                    Vector3 w2s_enemy = camera.WorldToScreenPoint(target);

                    if (w2s_enemy.z > -1)
                    {
                        NullsRenderer.DrawTracer(w2s_enemy, LocalVariables.colour, 1f, 2);
                    }
                }
            }  // if option is disabled or if there is only one target
        }  // end of ongui
    }  // end of tracer class
}
