using UnityEngine;

namespace CustomPalettes
{
    public struct SerializableColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public SerializableColor(Color c)
        {
            r = c.r * 255f;
            g = c.g * 255f;
            b = c.b * 255f;
            a = c.a * 255f;
        }

        public Color ToColor() => new Color(r / 255f, g / 255f, b / 255f, a / 255f);

        public static SerializableColor FromColor(Color c) => new SerializableColor(c);
    }

    public class CustomPaletteInfo
    {
        public string name;
        public SerializableColor[] colors;
    }
}