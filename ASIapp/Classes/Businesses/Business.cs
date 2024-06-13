using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes.Businesses
{
    using static ASIapp.Classes.Agent.AgentsLife;
    using static Util;
    public class Business : CellObject
    {

        public double IC_THR { get; set; }
        public double INV_A { get; set; }
        public int CAP_INC { get; set; }
        public double P_RISE { get; set; }
        public double AVAIL { get; set;  }
        public B_TYPE Type { get; set; }

        public Dictionary<B_TYPE, Dictionary<IqState, double>> BusinessAccept;




        public enum B_TYPE
        {
            Business1,
            Business2,
            Business3
        }
        
        public Business(B_TYPE bType)
        {

            BusinessAccept = new Dictionary<B_TYPE, Dictionary<IqState, double>>()
            {
                { 
                    B_TYPE.Business1, new Dictionary<IqState, double>
                    {
                        [IqState.Stupid] = (double)PAccB11,
                        [IqState.Standard] = (double)PAccB12,
                        [IqState.Clever] = (double)PAccB13,
                    }
                },
                {
                    B_TYPE.Business2, new Dictionary<IqState, double>
                    {
                        [IqState.Stupid] = (double)PAccB21,
                        [IqState.Standard] = (double)PAccB22,
                        [IqState.Clever] = (double)PAccB23,
                    }
                },
                {
                    B_TYPE.Business3, new Dictionary<IqState, double>
                    {
                        [IqState.Stupid] = (double)PAccB31,
                        [IqState.Standard] = (double)PAccB32,
                        [IqState.Clever] = (double)PAccB33,
                    }

                }
            };

            this.Type = bType;
            if (bType == B_TYPE.Business1)
            {
                IC_THR =  (double)ThrB1Val;
                INV_A =  (double)InvAB1Val;
                CAP_INC =  (int)IncreaseOfGapB1;
                P_RISE =  (double)PRiskB1;
;               AVAIL = (double)PAvailB1;
            }

            if (bType == B_TYPE.Business2)
            {
                IC_THR = (double)ThrB2Val;
                INV_A = (double)InvAB2Val;
                CAP_INC = (int)IncreaseOfGapB2;
                P_RISE = (double)PRiskB2;
                AVAIL = (double)PAvailB1 + (double)PAvailB2;
            }

            if (bType == B_TYPE.Business3)
            {
                IC_THR = (double)ThrB3Val;
                INV_A = (double)InvAB3Val;
                CAP_INC = (int)IncreaseOfGapB3;
                P_RISE = (double)PRiskB3;
                AVAIL = (double)PAvailB1 + (double)PAvailB2 + (double)PAvailB3;
            }


        }


    }
}
