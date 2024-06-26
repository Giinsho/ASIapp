﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ASIapp
{
    public class RectangleModel 
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Row { get; set; }
        public double Col { get; set; }
        public bool isAgent { get; set; }
        public Rectangle Rectangle { get; set; }
        public RectangleModel(double width, double height, double row, double col, Rectangle rectangle)
        {
            this.Width = width;
            this.Height = height;
            this.Row= row;
            this.Col = col;
            Rectangle = rectangle;
        }
    }
}
