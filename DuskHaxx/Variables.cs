using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx.variables
{
    class Menu
    {
        // Menu settings and default states
        public static string[] menu_entries_text = { "1. Godmode", "2. Aimbot (Test)", "3. Rapid fire", "4. Unlimited ammo", "5. Enemy ESP", "6. NoClip", "9. Position debug", "0. Fov debug" };
        public static int menu_entries = menu_entries_text.Length;

        public static bool menu_open = false;
        public static bool always_display_menu = false, menu_box = true;
        public static bool old_maincamera_mouselook;
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
        public static bool noclip_bool = false;
        public static bool player_pos_bool = true;
        public static bool player_fov_bool = true;
    }

    class CheatSettings
    {
        // Aimbot
        public static bool restore_rotation_after_aimbot = false;
        public static int aimbot_fov = 180;
        public static KeyCode aimbot_key1 = KeyCode.Mouse1;
        public static KeyCode aimbot_key2 = KeyCode.Mouse3;

        // Tracers
        public static bool draw_tracer_base = true;
        public static float tracer_base_thickness = 2f;
        public static bool draw_tracer_box_3d = false;
        public static float tracer_box_3d_thickness = 2f;

        // Noclip
        public static float noclip_speed = 2f;
        public static KeyCode noclip_key = KeyCode.V;
    }

    class Console
    {
        // Settings
        public static Vector2 console_size = new Vector2(500f, 22f);
        public static Vector2 console_position = new Vector2((float)(Screen.width / 2 - console_size.x/2), (float)(Screen.height - 130f));
        public static bool console_background = false;

        public static bool show_console = false;
    }
}
