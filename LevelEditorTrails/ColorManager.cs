using System.Collections.Generic;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorTrails
{
    public class ColorManager
    {
        private static readonly List<Color> validColors = new List<Color>()
        {
            new Color32(255, 0, 0, 255),
            new Color32(255, 128, 0, 255),
            new Color32(255, 255, 0, 255),
            new Color32(128, 255, 0, 255),
            new Color32(0, 255, 0, 255),
            new Color32(0, 255, 128, 255),
            new Color32(0, 255, 255, 255),
            new Color32(0, 128, 255, 255),
            new Color32(0, 0, 255, 255),
            new Color32(128, 0, 255, 255),
            new Color32(255, 0, 255, 255),
            new Color32(255, 0, 128, 255),
            new Color32(255, 255, 255, 255),
            new Color32(128, 128, 128, 255),
            new Color32(0, 0, 0, 255),
        };

        private static List<Color> availableColors = new List<Color>();

        public static Color GetColor()
        {
            if (availableColors.Count == 0)
                ResetColors();

            int index = Random.Range(0, availableColors.Count);
            Color color = availableColors[index];
            availableColors.RemoveAt(index);
            return color;
        }

        public static void ResetColors()
        {
            availableColors = new List<Color>(validColors);
        }
    }
}
