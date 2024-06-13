using ASIapp.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ASIapp
{
    public class RectangleModel 
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Rectangle Rectangle { get; set; }
        public List<CellObject> CellObject { get; set; }
        public RectangleModel(double width, double height, int row, int col, Rectangle rectangle)
        {
            this.Width = width;
            this.Height = height;
            this.Row= row;
            this.Col = col;
            Rectangle = rectangle;
            CellObject = new List<CellObject>();
        }
    }
}
