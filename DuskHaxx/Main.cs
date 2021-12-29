using System;
using UnityEngine;

// TODO: Draw dusk's cursor on top of the menu OR remove the cursor when stopping the camera and use the system one
// TODO: Fix tracers depending on the FOV (get the user's fov)
// TODO: Godmode
// TODO: Aimbot with move thing

namespace DuskHaxx
{
    class Main : MonoBehaviour
    {

        class LocalVariables
        {
            public static float old_xrotation, old_yrotation;
        }

        public void OnGUI()
        {
            // Watermark
            NullsRenderer.DrawWatermark(new Vector2(6f, 5f), "Null's dusk mod ", true);  // 2 lazy so i just add an space fr

            // Menu
            if (Input.GetKey(KeyCode.Insert) || variables.Menu.always_display_menu)
            {
                // Menu background
                Vector2 menu_pos = new Vector2(5f, 33f);
                Vector2 menu_size = new Vector2(240f, 18f * variables.Menu.menu_entries + 6f);  // 18 height per element
                Color menu_background_color = new Color(0.22f, 0.22f, 0.25f, 0.8f);
                Color menu_border_color = new Color(0.95f, 0f, 0f, 0.95f);
                NullsRenderer.DrawMenuBackground(variables.Menu.menu_box, menu_pos, menu_size, menu_background_color, menu_border_color);

                // Menu entries name (without state)
                for (int n = 0; n < variables.Menu.menu_entries; n++)
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + 3f, menu_pos.y + 18 * n), variables.Menu.menu_entries_text[n], Color.white, false);
                }

                // Hack state
                NullsRenderer.DrawMenuBoolState(0, menu_pos, menu_size, variables.CheatState.godmode_bool);
                NullsRenderer.DrawMenuBoolState(1, menu_pos, menu_size, variables.CheatState.aimbot_bool);
                NullsRenderer.DrawMenuBoolState(2, menu_pos, menu_size, variables.CheatState.rapidfire_bool);
                NullsRenderer.DrawMenuBoolState(3, menu_pos, menu_size, variables.CheatState.unlimited_ammo_bool);
                NullsRenderer.DrawMenuBoolState(4, menu_pos, menu_size, variables.CheatState.tracer_bool);
                NullsRenderer.DrawMenuBoolState(5, menu_pos, menu_size, variables.CheatState.player_pos_bool);
            }

            // Show cursor while holding insert
            if (Input.GetKey(KeyCode.Insert))
            {
                // First frame we press insert
                if (!variables.Menu.menu_open)
                {
                    MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                    LocalVariables.old_xrotation = myMouseLook.xRotation;
                    LocalVariables.old_yrotation = myMouseLook.yRotation;

                    variables.Menu.old_cursor_visible = Cursor.visible;
                    variables.Menu.old_cursorlockmode = Cursor.lockState;
                    variables.Menu.old_maincamera_mouselook = GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled;
                }
                
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = false;

                variables.Menu.menu_open = true;
            // After we release it
            } else if (variables.Menu.menu_open) {
                Cursor.visible = variables.Menu.old_cursor_visible;
                Cursor.lockState = variables.Menu.old_cursorlockmode;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = variables.Menu.old_maincamera_mouselook;

                MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                myMouseLook.xRotation = LocalVariables.old_xrotation;
                myMouseLook.yRotation = LocalVariables.old_yrotation;

                variables.Menu.menu_open = false;
            }
        }

        public void Update()
        {
            // Unload cheat
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Loader.Unload();
            }

            // Menu options
            if (Input.GetKey(KeyCode.Insert))
            {
                variables.CheatState.godmode_bool = ToggleVariable("1", variables.CheatState.godmode_bool);
                variables.CheatState.aimbot_bool = ToggleVariable("2", variables.CheatState.aimbot_bool);
                variables.CheatState.rapidfire_bool = ToggleVariable("3", variables.CheatState.rapidfire_bool);
                variables.CheatState.unlimited_ammo_bool = ToggleVariable("4", variables.CheatState.unlimited_ammo_bool);
                variables.CheatState.tracer_bool = ToggleVariable("5", variables.CheatState.tracer_bool);
                variables.CheatState.player_pos_bool = ToggleVariable("0", variables.CheatState.player_pos_bool);
            }
        }

        public static bool ToggleVariable(string toggle_key, bool bool_to_toggle)
        {
            if (Input.GetKeyDown(toggle_key))
            {
                return !bool_to_toggle;
            } else {
                return bool_to_toggle;
            }

        }
    }
}
