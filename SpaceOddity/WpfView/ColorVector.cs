using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WpfView
{
    public struct ColorVector
    {
        public double Red { get; private set; }
        public double Green { get; private set; }
        public double Blue { get; private set; }

        public ColorVector(double red, double green, double blue) : this()
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public Color GetColor()
        {
            return Color.FromRgb(GetByte(Red), GetByte(Green), GetByte(Blue));
        }

        private byte GetByte(double value)
        {
            return (byte)(value * 255);
        }
    }
}
