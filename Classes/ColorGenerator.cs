using System;
using System.Windows.Media;

namespace Classes
{
    internal class ColorGenerator
    {
        public static Color GetRandColor(short vertexNumber) {

            var random = new Random(vertexNumber);
            byte[] colorBytes = new byte[3];
            colorBytes[0] = (byte)(random.Next(128) + 127);
            colorBytes[1] = (byte)(random.Next(128) + 127);
            colorBytes[2] = (byte)(random.Next(128) + 127);

            Color color = new Color {

                A = 255,
                R = colorBytes[0],
                B = colorBytes[1],
                G = colorBytes[2]
            };

            return color;
        }
    }
}
