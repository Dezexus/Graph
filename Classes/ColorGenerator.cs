using System;
using System.Windows.Media;

namespace Classes
{
    internal class ColorGenerator
    {
        private static int Count = 0; 

        public static Color GetRandColor() {

            Count++;
            var random = new Random(Count);
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
