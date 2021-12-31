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

            //public static Color console_background_color = new Color(0.05f, 0.05f, 0.05f, 1f);
            //public static Color console_border_color = new Color(0.95f, 0f, 0f, 0.95f);
            public static Rect input_field_rect = new Rect(variables.Console.console_position + new Vector2(10f, 0f), variables.Console.console_size - new Vector2(6f, 0f));

            public static string console_input;
        }

        public void Update()
        {
            // Check if the user presses enter
            if (variables.Console.show_console && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Escape))
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
                    variables.Menu.menu_background_color, variables.Menu.menu_border_color, 1f);

                // Background text if no text
                if (LocalVariables.console_input == "")
                {
                    Vector2 console_text_pos = variables.Console.console_position + new Vector2(13f, 1f); //3f, 1f
                    ExtRender.DrawString(console_text_pos, "Command...", new Color(0.8f, 0.8f, 0.8f, 0.7f), false);
                }
                ExtRender.DrawString(variables.Console.console_position + new Vector2(3f, 1f), ">", new Color(0.9f, 0.9f, 0.9f, 0.9f), false);

                // Actual input
                GUI.SetNextControlName("ConsoleField");
                GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
                GUI.color = new Color(0.95f, 0.95f, 0.95f, 0.95f);
                LocalVariables.console_input = GUI.TextField(LocalVariables.input_field_rect, LocalVariables.console_input, 255);
                GUI.FocusControl("ConsoleField");

            } else if (LocalVariables.aux_openconsole) { // After we close the console (pressing enter or the toggle console hotkey)
                Time.timeScale = LocalVariables.old_timescale;
                LocalVariables.aux_openconsole = false;
            }
        }
    }
}
