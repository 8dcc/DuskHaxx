using System;
using UnityEngine;

namespace DuskHaxx
{
    class Console : MonoBehaviour
    {
        class LocalVariables
        {
            public static float old_timescale;
            public static bool aux_openconsole = false;

            public static Color console_background_color = new Color(0.05f, 0.05f, 0.05f, 1f); // 0.15f, 0.15f, 0.16f, 0.95f
            public static Color console_border_color = new Color(0.95f, 0f, 0f, 0.95f);
            public static Rect input_field_rect = new Rect(variables.Console.console_position, variables.Console.console_size);

            public static string console_input;
        }

        public void Update()
        {
            // Check if the user presses enter
            if (variables.Console.show_console && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Escape)))
            {
                if (LocalVariables.console_input != "")
                {
                    CommandProcessor.ParseCommand(LocalVariables.console_input);
                }
                variables.Console.show_console = false;
            }
        }

        public void OnGUI()
        {
            if (variables.Console.show_console)
            {
                if (!LocalVariables.aux_openconsole) // First time we open the console
                {
                    // Clear the input field
                    LocalVariables.console_input = "";
                    // Stop time while the console is active
                    LocalVariables.old_timescale = Time.timeScale;
                    Time.timeScale = 0;

                    LocalVariables.aux_openconsole = true;
                }

                // Background
                NullsRenderer.DrawMenuBackground(variables.Console.console_background, variables.Console.console_position, variables.Console.console_size,
                    LocalVariables.console_background_color, LocalVariables.console_border_color, 1f);

                // Background text if no text
                if (LocalVariables.console_input == "")
                {
                    Vector2 console_text_pos = variables.Console.console_position + new Vector2(3f, 1f);
                    ExtRender.DrawString(console_text_pos, "Command...", false);
                }

                // Actual input
                LocalVariables.console_input = GUI.TextField(LocalVariables.input_field_rect, LocalVariables.console_input, 255);

            } else if (LocalVariables.aux_openconsole) { // After we close the console (pressing enter or the toggle console hotkey)
                Time.timeScale = LocalVariables.old_timescale;
                LocalVariables.aux_openconsole = false;
            }
        }
    }
}
