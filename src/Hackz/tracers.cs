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

            public static bool old_fov_bool, aux_first_time = true;
        }

        public void Update()
        {
            if (variables.CheatState.tracer_bool)
            {
                LocalVariables.enemyPos.Clear();

                foreach (BasicAIScript enemy in UnityEngine.Object.FindObjectsOfType(typeof(BasicAIScript)) as BasicAIScript[])
                {
                    // Apply offset
                    Vector3 anime_feet = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 1.15f, enemy.transform.position.z);
                    LocalVariables.enemyPos.Add(anime_feet);
                }
            }
        }

        public void OnGUI()
        {
            if (variables.CheatState.tracer_bool && LocalVariables.enemyPos.Count > 0)
            {
                if (LocalVariables.aux_first_time)
                {
                    LocalVariables.old_fov_bool = variables.CheatState.player_fov_bool;
                    variables.CheatState.player_fov_bool = true;
                    LocalVariables.aux_first_time = false;
                }

                // Get the camera and draw the tracer
                Camera camera = FindObjectOfType<Camera>();
                foreach (Vector3 target in LocalVariables.enemyPos)
                {
                    Vector3 w2s_enemy = camera.WorldToScreenPoint(target);
                    float distance_to_target = Vector3.Distance(FindObjectOfType<Camera>().transform.position, target);

                    if (w2s_enemy.z > -1 && (variables.CheatSettings.max_tracer_distance == 0f || distance_to_target < variables.CheatSettings.max_tracer_distance))
                    {
                        NullsRenderer.DrawTracer(new Vector2(w2s_enemy.x, w2s_enemy.y), LocalVariables.colour, 1.5f, 2);

                        // Draw tracer X base
                        if (variables.CheatSettings.draw_tracer_base || variables.CheatSettings.draw_tracer_box_3d)
                        {
                            Vector3 w2s_enemy_base_x1 = camera.WorldToScreenPoint(new Vector3(target.x - 1f, target.y, target.z));
                            Vector3 w2s_enemy_base_x2 = camera.WorldToScreenPoint(new Vector3(target.x + 1f, target.y, target.z));
                            Vector3 w2s_enemy_base_z1 = camera.WorldToScreenPoint(new Vector3(target.x, target.y, target.z - 1f));
                            Vector3 w2s_enemy_base_z2 = camera.WorldToScreenPoint(new Vector3(target.x, target.y, target.z + 1f));

                            // Check if the x is in the screen
                            if (w2s_enemy_base_x1.z > -1 && w2s_enemy_base_z1.z > -1 && w2s_enemy_base_x2.z > -1 && w2s_enemy_base_z2.z > -1)
                            {
                                ExtRender.DrawLine(new Vector2(w2s_enemy_base_x1.x, (float)Screen.height - w2s_enemy_base_x1.y),
                                        new Vector2(w2s_enemy_base_x2.x, (float)Screen.height - w2s_enemy_base_x2.y), LocalVariables.colour, variables.CheatSettings.tracer_base_thickness);
                                ExtRender.DrawLine(new Vector2(w2s_enemy_base_z1.x, (float)Screen.height - w2s_enemy_base_z1.y),
                                        new Vector2(w2s_enemy_base_z2.x, (float)Screen.height - w2s_enemy_base_z2.y), LocalVariables.colour, variables.CheatSettings.tracer_base_thickness);

                                // Draw "tracer" box 3d
                                if (variables.CheatSettings.draw_tracer_box_3d)
                                {
                                    Vector3 w2s_enemy_top_x1 = camera.WorldToScreenPoint(new Vector3(target.x - 1f, target.y + 2.6f, target.z));
                                    Vector3 w2s_enemy_top_x2 = camera.WorldToScreenPoint(new Vector3(target.x + 1f, target.y + 2.6f, target.z));
                                    Vector3 w2s_enemy_top_z1 = camera.WorldToScreenPoint(new Vector3(target.x, target.y + 2.6f, target.z - 1f));
                                    Vector3 w2s_enemy_top_z2 = camera.WorldToScreenPoint(new Vector3(target.x, target.y + 2.6f, target.z + 1f));

                                    if (w2s_enemy_top_x1.z > -1 && w2s_enemy_top_z1.z > -1 && w2s_enemy_top_x2.z > -1 && w2s_enemy_top_z2.z > -1)
                                    {
                                        // Upper X
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_top_x1.x, (float)Screen.height - w2s_enemy_top_x1.y),
                                                new Vector2(w2s_enemy_top_x2.x, (float)Screen.height - w2s_enemy_top_x2.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_top_z1.x, (float)Screen.height - w2s_enemy_top_z1.y),
                                                new Vector2(w2s_enemy_top_z2.x, (float)Screen.height - w2s_enemy_top_z2.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);

                                        // 3d box walls
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_base_x1.x, (float)Screen.height - w2s_enemy_base_x1.y),
                                                new Vector2(w2s_enemy_top_x1.x, (float)Screen.height - w2s_enemy_top_x1.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_base_x2.x, (float)Screen.height - w2s_enemy_base_x2.y),
                                                new Vector2(w2s_enemy_top_x2.x, (float)Screen.height - w2s_enemy_top_x2.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_base_z1.x, (float)Screen.height - w2s_enemy_base_z1.y),
                                                new Vector2(w2s_enemy_top_z1.x, (float)Screen.height - w2s_enemy_top_z1.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);
                                        ExtRender.DrawLine(new Vector2(w2s_enemy_base_z2.x, (float)Screen.height - w2s_enemy_base_z2.y),
                                                new Vector2(w2s_enemy_top_z2.x, (float)Screen.height - w2s_enemy_top_z2.y), LocalVariables.colour, variables.CheatSettings.tracer_box_3d_thickness);
                                    }  // end of 3d box z check
                                }  // end of 3d box
                            }  // end of X base z check
                        }  // end of X base check

                        if (variables.CheatSettings.draw_tracer_distance)
                        {
                            Vector3 w2s_enemy_distance_pos = camera.WorldToScreenPoint(new Vector3(target.x, target.y + 0.2f, target.z));
                            string distance_string = distance_to_target.ToString("0");

                            if (w2s_enemy_distance_pos.z > -1)
                            {
                                ExtRender.DrawString(new Vector2(w2s_enemy_distance_pos.x, (float)Screen.height - w2s_enemy_distance_pos.y),
                                        distance_string, variables.Menu.menu_border_color);
                            }  // end of w2s_enemy_distance_pos z check
                        }  // end of draw_tracer_distance check
                    }  // end of w2s_enemy z check
                }  // end of foreach enemy

            }
            else if (!LocalVariables.aux_first_time)        // If tracers are toggled off
            {
                variables.CheatState.player_fov_bool = LocalVariables.old_fov_bool;
                LocalVariables.aux_first_time = true;
            }
        }
    }
}
