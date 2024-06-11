using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes.Businesses
{
    public class Business1 : Business
    {
        public static double Availability { get; set; }

        public Business1(CellObject cell)
        {
            ID = cell.ID;
            GLOBAL_ID = cell.GLOBAL_ID;
        }
    }

    public class Business2 : Business
    {
        public static double Availability { get; set; }

        public Business2(CellObject cell)
        {
            ID = cell.ID;
            GLOBAL_ID = cell.GLOBAL_ID;
        }
    }

    public class Business3 : Business
    {
        public static double Availability { get; set; }

        public Business3(CellObject cell)
        {
            ID = cell.ID;
            GLOBAL_ID = cell.GLOBAL_ID;
        }
    }
}
