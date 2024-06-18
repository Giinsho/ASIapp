using System.Transactions;
using ASIapp.Classes.Businesses;

namespace ASIapp.Classes.Agent
{
    using static Util;

    public class Agent : AgentsLife
    {
    
        public int IQ { get; set; }

        public IqState IQ_STATE { get; set; }
        public HealthState H_STATE { get; set; }
        public double DISEASE { get; set; }
        public double MOBILITY { get; set; }
        public double CAPITAL { get; set; }

        public int Counter { get; set; } = 0;

        public  double INIT_CAPITAL = InitCapitIc;

        public WealthState WealthState = WealthState.Poor;

        public double rAccB1_aprofile = 0.0,  rAccB2_aprofile = 0.0, rAccB3_aprofile = 0.0;
        public bool aprofile = false;


        public Agent()
        {
            IQ = (int)RandomGen.Gauss(IqMean, IqRangeMin, IqRangeMax);
            dynamic rand = RandomGen.Next();

            if (rand <= IqStateThresholds[IqState.Stupid])
            {
                IQ_STATE = IqState.Stupid;
            }
            else if (rand <= IqStateThresholds[IqState.Standard])
            {
                IQ_STATE = IqState.Standard;
            }
            else
            {
                IQ_STATE = IqState.Clever;
            }


            rand = RandomGen.NextDouble();

            if (rand <= HealthStateThresholds[HealthState.Weak])
            {
                H_STATE = HealthState.Weak;
            }
            else if (rand <= HealthStateThresholds[HealthState.Standard])
            {
                H_STATE = HealthState.Standard;
            }
            else
            {
                H_STATE = HealthState.Good;
            }


            MOBILITY = MobilityThresholds[IQ_STATE];

            CAPITAL = INIT_CAPITAL;

            DISEASE = DiseaseThresholds[H_STATE];

        }

        public Agent(CellObject cell) : this()
        {
            ID = cell.ID;
            GLOBAL_ID = cell.GLOBAL_ID;
        }

        public void WealthStateUpdate()
        {
            double thresholdPoor = WealthThresholds[WealthState.Poor] * INIT_CAPITAL;
            double thresholdFair = WealthThresholds[WealthState.Fair] * INIT_CAPITAL;

            if (CAPITAL <= thresholdPoor)
            {
                WealthState = WealthState.Poor;
            }
            else if (CAPITAL > thresholdPoor && CAPITAL <= thresholdFair)
            {
                WealthState = WealthState.Fair;
            }
            else
            {
                WealthState = WealthState.Rich;
            }
        }

        public bool IsAgentFair()
        {
            double thresholdPoor = WealthThresholds[WealthState.Poor] * INIT_CAPITAL;
            double thresholdFair = WealthThresholds[WealthState.Fair] * INIT_CAPITAL;
            return CAPITAL > thresholdPoor && CAPITAL <= thresholdFair;
        }

        public bool IsAgentPoor()
        {
            double threshold = WealthThresholds[WealthState.Poor] * INIT_CAPITAL;
            return CAPITAL <= threshold;
        }

        public bool IsAgentRich()
        {
            double threshold = WealthThresholds[WealthState.Fair] * INIT_CAPITAL;
            return CAPITAL > threshold;
        }
    }
}
