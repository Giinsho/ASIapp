using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIapp.Classes.Agent
{
    using static Util;

    public abstract class AgentsLife : CellObject
    {

        public enum HealthState
        {
            Weak = 1,
            Standard = 2,
            Good = 3
        }

        public enum IqState
        {
            Stupid,
            Standard,
            Clever
        }

        public enum WealthState
        {
            Poor,
            Fair,
            Rich
        }

        public int IterationsOnBusiness;
        public double DecreaseInitialCapitalOnDisease;
        protected int IqRangeMin;
        protected int IqRangeMax;
        protected double IqMean;


        protected Dictionary<HealthState, double> HealthStateThresholds = new();
        protected Dictionary<HealthState, double> DiseaseThresholds = new();
        protected Dictionary<IqState, double> IqStateThresholds = new();
        protected List<Dictionary<IqState, double>> BusinessesAccepts = new();
        protected Dictionary<IqState, double> MobilityThresholds = new();
        public Dictionary<WealthState, double> WealthThresholds = new();

        public AgentsLife()
        {
            GetValuesFromInputs();
            IqMean = (double)(IqRangeMax + IqRangeMin) / 2;
        }


        private void GetValuesFromInputs()
        {
            IterationsOnBusiness = (int)NumberIterSuspB;
            DecreaseInitialCapitalOnDisease = (double)NumberDecRate;
            IqRangeMin = (int)MinIqRange;
            IqRangeMax = (int)MaxIqRange;

            HealthStateThresholds = new Dictionary<HealthState, double>
            {
                [HealthState.Weak] = (double)PHS1,
                [HealthState.Standard] = (double)PHS1 + (double)PHS2,
                [HealthState.Good] = (double) PHS1 + (double)PHS2 + (double)PHS3,
            };

            DiseaseThresholds = new Dictionary<HealthState, double>
            {
                [HealthState.Weak] = (double)PIll1,
                [HealthState.Standard] = (double)PIll2,
                [HealthState.Good] = (double)PIll3,
            };

            IqStateThresholds = new Dictionary<IqState, double>
            {
                [IqState.Stupid] = 0,
                [IqState.Standard] = (double) IqLeThan,
                [IqState.Clever] = (double) IqGtThan,
            };

            BusinessesAccepts = new List<Dictionary<IqState, double>>
            {
                new Dictionary<IqState, double>
                {
                    [IqState.Stupid] = (double)PAccB11,
                    [IqState.Standard] = (double)PAccB12,
                    [IqState.Clever] = (double)PAccB13,
                },
                new Dictionary<IqState, double>
                {
                    [IqState.Stupid] = (double)PAccB21,
                    [IqState.Standard] = (double)PAccB22,
                    [IqState.Clever] = (double)PAccB23,
                },
                new Dictionary<IqState, double>
                {
                    [IqState.Stupid] = (double)PAccB31,
                    [IqState.Standard] = (double)PAccB32,
                    [IqState.Clever] = (double)PAccB33,
                }
            };

            MobilityThresholds = new Dictionary<IqState, double>
            {
                [IqState.Stupid] = (double)PMobility1,
                [IqState.Standard] = (double)PMobility2,
                [IqState.Clever] = (double)PMobility3,
            };

            WealthThresholds = new Dictionary<WealthState, double>
            {
                [WealthState.Poor] = (double) WealthPooper,
                [WealthState.Fair] = (double) WealthFeasible,
                [WealthState.Rich] = (double) WealthRich,
            };

        }
    }
}
