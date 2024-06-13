using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes
{
    public class CellObject
    {
        public int ID { get; set; }
        public int GLOBAL_ID { get; set; }

        public CellObject()
        {

        }

        public CellObject(int n_of_col,int col, int row)
        {
            GLOBAL_ID = CalculateGlobalID(n_of_col, col, row);
        }

        public static int CalculateGlobalID(int n_of_col, int col, int row)
        {
            return n_of_col * (row - 1) + col;
        }

        public Point DecalculateGlobalID(int n_of_cols, int n_of_rows)
        {
            var row = int.Parse(Math.Floor(GLOBAL_ID*1.00 / n_of_rows*1.00).ToString());
            var col = GLOBAL_ID % n_of_cols;

            return new Point(col, row);
        }
    }
}
