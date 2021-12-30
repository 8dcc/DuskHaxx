using UnityEngine;

namespace DuskHaxx
{
	// Made it myself cuz im cool as fuck
	public class NullsRenderer : MonoBehaviour
	{
		public static void DrawWatermark(Vector2 position, Vector2 size, string label, bool background = true)
		{
			if (background)
			{
				Vector2 background_pos = new Vector2(position.x - 2f, position.y - 1f);
				Vector2 background_size = new Vector2(size.x + 17f, size.y + 1f);
				ExtRender.DrawBox(background_pos, background_size, new Color(0.22f, 0.22f, 0.23f, 0.8f));  // Background
				ExtRender.DrawBoxOutline(background_pos, background_size.x, background_size.y, new Color(0.95f, 0f, 0f, 0.95f), 1f);

				ExtRender.DrawString(position, label, new Color(0.95f, 0.95f, 0.95f), false);
			}
			else
			{
				ExtRender.DrawString(position, label, new Color(0.95f, 0f, 0f, 0.95f), false);
			}
		}
		public static void DrawWatermark(Vector2 position, string label, bool background = true)
		{
			Vector2 size = new Vector2((float)label.Length * 6.3f, 21f);
			if (background)
			{
				Vector2 background_pos = new Vector2(position.x - 2f, position.y);
				Vector2 background_size = new Vector2(size.x, size.y);
				ExtRender.DrawBox(background_pos, background_size, new Color(0.22f, 0.22f, 0.23f, 0.8f));  // Background
				ExtRender.DrawBoxOutline(background_pos, background_size.x, background_size.y, new Color(0.95f, 0f, 0f, 0.95f), 1f);

				ExtRender.DrawString(position, label, new Color(0.95f, 0.95f, 0.95f), false);
			} else {
				ExtRender.DrawString(position, label, new Color(0.95f, 0f, 0f, 0.95f), false);
			}
		}
		public static void DrawMenuBoolState(int index, Vector2 menu_pos, Vector2 menu_size, bool state)
        {
			if (state)
			{
				ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 23f, menu_pos.y + 18 * index), "ON", Color.green, false);
			} else {
				ExtRender.DrawString(new Vector2(menu_pos.x + menu_size.x - 30f, menu_pos.y + 18 * index), "OFF", Color.red, false);
			}
		}
		public static void DrawMenuBackground(bool enabled, Vector2 position, Vector2 size, Color background_color, Color border_color, float border_thickness = 1f )
        {
			if (enabled)  // This variables is used to check if you want to render the menu with background or not, NOT to check if you want to render the background
			{
				ExtRender.DrawBox(position, size, background_color);  // Background
				ExtRender.DrawBoxOutline(position, size.x, size.y, border_color, border_thickness);  // Border
			}
		}
		public static void DrawTracer(Vector2 w2s_target, Color colour, float thickness = 1f, int tracer_origin = 1)
		{
			switch (tracer_origin)
			{
				case 0: // Top (ur a monster)
					ExtRender.DrawLine(new Vector2((float)(Screen.width / 2), 0f), new Vector2(w2s_target.x, (float)Screen.height - w2s_target.y), colour, thickness);
					break;
				case 1: // Crosshair
					ExtRender.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s_target.x, (float)Screen.height - w2s_target.y), colour, thickness);
					break;
				case 2: // Bottom
					ExtRender.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height)), new Vector2(w2s_target.x, (float)Screen.height - w2s_target.y), colour, thickness);
					break;
				default:
					break;
			}
		}
	}

	/* ---------------------------------------------------- */

	public class ExtRender : MonoBehaviour
	{
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
		{
			Color = color;
			DrawBox(position, size, centered);
		}
		public static void DrawBox(Vector2 position, Vector2 size, bool centered = true)
		{
			var upperLeft = centered ? position - size / 2f : position;
			GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
		}

		public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
		{
			Color = color;
			DrawString(position, label, centered);
		}
		public static void DrawString(Vector2 position, string label, bool centered = true)
		{
			var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			GUI.Label(new Rect(upperLeft, size), content);
		}

		public static Texture2D lineTex;
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		public static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
		{
			DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		public static void DrawBoxOutline(Vector2 Point, float width, float height, Color color, float thickness)
		{
			DrawLine(Point, new Vector2(Point.x + width, Point.y), color, thickness);
			DrawLine(Point, new Vector2(Point.x, Point.y + height), color, thickness);
			DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x + width, Point.y), color, thickness);
			DrawLine(new Vector2(Point.x + width, Point.y + height), new Vector2(Point.x, Point.y + height), color, thickness);
		}
	}
}
