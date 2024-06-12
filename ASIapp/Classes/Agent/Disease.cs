﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ASIapp.Classes
{

    
    public class Disease : CellObject
    {

        private Disease() { }
        public Disease(CellObject element) : this()
        {
            ID = element.ID;
            GLOBAL_ID = element.GLOBAL_ID;
        }


    }
}