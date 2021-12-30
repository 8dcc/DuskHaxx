using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx.variables
{
    class Menu
    {
        // Menu settings and default states
        public static string[] menu_entries_text = { "1. Godmode", "2. Aimbot (Test)", "3. Rapid fire", "4. Unlimited ammo", "5. Enemy ESP", "0. Position debug" };
        public static int menu_entries = menu_entries_text.Length;

        public static bool menu_open = false;
        public static bool always_display_menu = false, menu_box = true;
        public static bool old_cursor_visible, old_maincamera_mouselook;
        public static CursorLockMode old_cursorlockmode;
    }

    class CheatState
    {
        // Cheat bools
        public static bool godmode_bool = false;
        public static bool aimbot_bool = false;
        public static bool unlimited_ammo_bool = false;
        public static bool rapidfire_bool = false;
        public static bool tracer_bool = true;
        public static bool player_pos_bool = true;
    }

    class CheatSettings
    {
        public static int aimbot_fov = 180;
        public static KeyCode aimbot_key1 = KeyCode.Mouse1;
        public static KeyCode aimbot_key2 = KeyCode.Mouse3;
    }
}
