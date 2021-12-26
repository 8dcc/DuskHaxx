using System;
using UnityEngine;

namespace DuskHaxx
{
    class Main : MonoBehaviour
    {
        // Menu settings and default states
        int menu_entries = 3;
        string[] menu_entries_text = { "1. Rotate camera (Test)", "2. Unlimited ammo", "3. Rapid fire" };
        
        bool menu_open = false;
        bool always_display_menu = false, menu_box = true;
        bool old_cursor_visible, old_maincamera_mouselook;
        CursorLockMode old_cursorlockmode;

        // Cheat bools
        bool aimbot_bool = false, unlimited_ammo_bool = false, rapidfire_bool = false, nocooldown_bool = false;

        public void OnGUI()
        {
            // Watermark
            NullsRenderer.DrawWatermark(new Vector2(5f, 5f), new Vector2(110f, 20f), "Null's dusk mod", false);

            // Menu
            if (Input.GetKey(KeyCode.Insert) || always_display_menu)
            {
                // Menu background
                Vector2 menu_pos = new Vector2(5f, 33f);
                Vector2 menu_size = new Vector2(200f, 20f * menu_entries + 6f);  // 20 height per element
                if (menu_box)  // Ugly in-game
                {
                    ExtRender.DrawBox(menu_pos, menu_size,  new Color(0.22f, 0.22f, 0.25f, 0.8f));  // Background
                    ExtRender.DrawBoxOutline(menu_pos, menu_size.x, menu_size.y, new Color(0.95f, 0f, 0f, 0.95f), 1.5f);
                    //GUI.Box(new Rect(menu_pos.x, menu_pos.y - 3f, menu_size.x, menu_size.y), "");
                }

                // Menu entries (without state)
                for (int n = 0; n < menu_entries; n++)
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + 4f, menu_pos.y + 20 * n), menu_entries_text[n], Color.white, false);
                }

                // Hack state
                if (aimbot_bool)
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 23f, menu_pos.y), "ON", Color.green, false);
                }
                else
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 30f, menu_pos.y), "OFF", Color.red, false);
                }
                if (unlimited_ammo_bool)
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 23f, menu_pos.y + 20 * 1), "ON", Color.green, false);
                }
                else
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 30f, menu_pos.y + 20 * 1), "OFF", Color.red, false);
                }
                if (rapidfire_bool)
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 23f, menu_pos.y + 20 * 2), "ON", Color.green, false);
                }
                else
                {
                    ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 30f, menu_pos.y + 20 * 2), "OFF", Color.red, false);
                }
            }

            /* ------------------------------------------------------------------ */
            if (Input.GetKey(KeyCode.Insert))
            {
                if (!menu_open)
                {
                    old_cursor_visible = Cursor.visible;
                    old_cursorlockmode = Cursor.lockState;
                    old_maincamera_mouselook = GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled;
                }
                
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = false;

                menu_open = true;
            } else if (menu_open) {
                Cursor.visible = old_cursor_visible;
                Cursor.lockState = old_cursorlockmode;
                GameObject.Find("MainCamera").GetComponent<MyMouseLook>().enabled = old_maincamera_mouselook;

                menu_open = false;
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Loader.Unload();
            }

            // Menu options
            if (Input.GetKey(KeyCode.Insert))
            {
                // Aimbot?
                if (Input.GetKeyDown("1"))
                {
                    aimbot_bool = !aimbot_bool;
                }
                // InfAmmo
                if (Input.GetKeyDown("2"))
                {
                    unlimited_ammo_bool = !unlimited_ammo_bool;
                }
                // RapidFire
                if (Input.GetKeyDown("3"))
                {
                    rapidfire_bool = !rapidfire_bool;
                }
                // No weapon cooldown
                if (Input.GetKeyDown("4"))
                {
                    nocooldown_bool = !nocooldown_bool;
                }
            }
        }
    }
}
