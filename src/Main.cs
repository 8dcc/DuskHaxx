﻿using System;
using UnityEngine;

// TODO: Draw dusk's cursor on top of the menu OR remove the cursor when stopping the camera and use the system one

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

                NullsRenderer.DrawMenuBackground(variables.Menu.menu_box, menu_pos, menu_size, variables.Menu.menu_background_color, variables.Menu.menu_border_color);

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
                NullsRenderer.DrawMenuBoolState(5, menu_pos, menu_size, variables.CheatState.noclip_bool);
                NullsRenderer.DrawMenuBoolState(6, menu_pos, menu_size, variables.CheatState.player_pos_bool);
                NullsRenderer.DrawMenuBoolState(7, menu_pos, menu_size, variables.CheatState.player_fov_bool);
            }

            // Show cursor while holding insert
            if (Input.GetKey(KeyCode.Insert) || variables.Console.show_console)
            {
                // First frame we press insert
                if (!variables.Menu.menu_open)
                {
                    variables.Menu.old_cursorlockmode = Cursor.lockState;
                    variables.Menu.old_maincamera_mouselook = GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled;

                    // Save old camera rotation
                    MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                    LocalVariables.old_xrotation = myMouseLook.xRotation;
                    LocalVariables.old_yrotation = myMouseLook.yRotation;
                }

                // Stop moving camera and release cursor
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = false;

                if (!variables.Menu.always_display_menu)
                {
                    variables.Menu.menu_open = true;
                }
                // After we release it
            }
            else if (variables.Menu.menu_open)
            {
                Cursor.lockState = variables.Menu.old_cursorlockmode;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = variables.Menu.old_maincamera_mouselook;

                // Restore old camera rotation
                MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                myMouseLook.xRotation = LocalVariables.old_xrotation;
                myMouseLook.yRotation = LocalVariables.old_yrotation;
                if (!variables.Menu.always_display_menu) variables.Menu.menu_open = false;
            }
        }

        public void Update()
        {
            // Unload cheat
            if (Input.GetKeyDown(KeyCode.Delete)) Loader.Unload();

            // Menu option toggle
            if (Input.GetKey(KeyCode.Insert))
            {
                // Hackz
                variables.CheatState.godmode_bool = ToggleVariable("1", variables.CheatState.godmode_bool);
                variables.CheatState.aimbot_bool = ToggleVariable("2", variables.CheatState.aimbot_bool);
                variables.CheatState.rapidfire_bool = ToggleVariable("3", variables.CheatState.rapidfire_bool);
                variables.CheatState.unlimited_ammo_bool = ToggleVariable("4", variables.CheatState.unlimited_ammo_bool);
                variables.CheatState.tracer_bool = ToggleVariable("5", variables.CheatState.tracer_bool);
                variables.CheatState.noclip_bool = ToggleVariable("6", variables.CheatState.noclip_bool);

                // Misc
                variables.CheatState.player_pos_bool = ToggleVariable("9", variables.CheatState.player_pos_bool);
                variables.CheatState.player_fov_bool = ToggleVariable("0", variables.CheatState.player_fov_bool);

                // Console
                variables.Console.show_console = ToggleVariable("k", variables.Console.show_console);
            }
        }

        public static bool ToggleVariable(string toggle_key, bool bool_to_toggle)
        {
            return (Input.GetKeyDown(toggle_key)) ? !bool_to_toggle : bool_to_toggle;
        }
    }
}
