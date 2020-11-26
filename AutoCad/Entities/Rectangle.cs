using System;
using System.Collections.Generic;
using System.Text;

namespace Edsa.AutoCadProxy
{
    public class Rectangle
    {
        public Rectangle(double top, double bottom, double left, double right)
        {
            this.Top = top;
            this.Bottom = bottom;
            this.Left = left;
            this.Right = right;
        }

        public double Top { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
    }
}
