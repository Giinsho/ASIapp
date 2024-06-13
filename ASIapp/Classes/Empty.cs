using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes
{
    public class Empty : CellObject
    {

       
            public Empty()
            {
                ID = -1;
                GLOBAL_ID = -1;
            }

        

            public Empty(CellObject cell) : this()
            {
                ID = cell.ID;
                GLOBAL_ID = cell.GLOBAL_ID;
            }
        
    }
}
