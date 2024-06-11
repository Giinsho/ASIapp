using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes.Businesses
{
    using static Util;
    public abstract class Business : CellObject
    {

        public enum B_TYPE
        {
            Business1,
            Business2,
            Business3

        }

        public double IC_THR { get; set; }
        public double INV_A { get; set; }
        public int CAP_INC { get; set; }
        public double P_RISE { get; set; }

        private Dictionary<B_TYPE, List<double>> Values = new()
        {
            {B_TYPE.Business1, new List<double>
                    { (double) ThrB1Val,
                (double) InvAB1Val,
                (int) IncreaseOfGapB1,
                (double) PRiskB1,
                (double) PAvailB1,


                    }
                },
            {B_TYPE.Business2, new List<double>
                { (double) ThrB2Val,
                    (double) InvAB2Val,
                    (int) IncreaseOfGapB2,
                    (double) PRiskB2,
                    (double) PAvailB1 + (double) PAvailB2,


                }
            },
            {B_TYPE.Business3, new List<double>
                { (double) ThrB3Val,
                    (double) InvAB3Val,
                    (int) IncreaseOfGapB3,
                    (double) PRiskB3,
                    (double) PAvailB1 + (double) PAvailB2 + (double) PAvailB3,


                }
            },
        };

        protected Business()
        {
            B_TYPE type = (B_TYPE)Enum.Parse(typeof(B_TYPE), GetType().Name);

            IC_THR = Values[type][0];
            INV_A = Values[type][1];
            CAP_INC = (int)Values[type][2];
            P_RISE = Values[type][3];
        }



    }
}
