using System;
using System.Windows.Media;

namespace Classes
{
    internal class ColorGenerator
    {
        private static int Count = 0; 

        public static Color GetRandColor()
        {
            Count++;
            Random rnd = new Random(Count);
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return (Color)ColorConverter.ConvertFromString("#" + hexOutput);
        }
    }
}
