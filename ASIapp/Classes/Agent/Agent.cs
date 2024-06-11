using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes.Agent
{
    public class Agent : CellObject
    {
        public int IQ { get; set; }
        public int H_STATE { get; set; }
        public double R_ACC_B1 { get; set; }
        public double R_ACC_B2 { get; set; }
        public double R_ACC_B3 { get; set; }
        public double MOBILITY { get; set; }
    }
}
