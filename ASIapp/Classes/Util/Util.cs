using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASIapp.Classes;

namespace ASIapp
{
    public static class Util
    {
        private static FirstPageWindow _firstPageWindow;

        public static void Initialize(FirstPageWindow firstPageWindow)
        {
            _firstPageWindow = firstPageWindow;
        }

        public static double Gauss(this Random random, double mean, double min, double max)
        {
            double u1 = 1.0 - random.NextDouble(); 
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2);
            double randNormal =
                mean + 14 * randStdNormal;
            double std = (randNormal - min) / (max - min);
            return std * (max - min) + min;
        }


        public static Random RandomGen;

        public static int NumberOfRows { get => _firstPageWindow.numberOfRows; set => _firstPageWindow.numberOfRows = value; }
        public static int NumberOfColumns { get => _firstPageWindow.numberOfColumns; set => _firstPageWindow.numberOfColumns = value; }
        public static int NumberOfA { get => _firstPageWindow.numberOfA; set => _firstPageWindow.numberOfA = value; }
        public static int NumberOfD { get => _firstPageWindow.numberOfD; set => _firstPageWindow.numberOfD = value; }
        public static int NumberOfB { get => _firstPageWindow.numberOfB; set => _firstPageWindow.numberOfB = value; }
        public static string CaStateFile { get => _firstPageWindow.caStateFile; set => _firstPageWindow.caStateFile = value; }
        public static string AProfileFile { get => _firstPageWindow.aProfileFile; set => _firstPageWindow.aProfileFile = value; }
        public static string RandNumFile { get => _firstPageWindow.randNumFile; set => _firstPageWindow.randNumFile = value; }
        public static int NumberOfIterations { get => _firstPageWindow.numberOfIterations; set => _firstPageWindow.numberOfIterations = value; }
        public static int NumberOfExperience { get => _firstPageWindow.numberOfExperience; set => _firstPageWindow.numberOfExperience = value; }
        public static bool IsTest1Selected { get => _firstPageWindow.isTest1Selected; set => _firstPageWindow.isTest1Selected = value; }
        public static bool IsTest2Selected { get => _firstPageWindow.isTest2Selected; set => _firstPageWindow.isTest2Selected = value; }
        public static bool IsTest3Selected { get => _firstPageWindow.isTest3Selected; set => _firstPageWindow.isTest3Selected = value; }
        public static bool IsClockSeedSelected { get => _firstPageWindow.isClockSeedSelected; set => _firstPageWindow.isClockSeedSelected = value; }
        public static bool IsCustomSeedSelected { get => _firstPageWindow.isCustomSeedSelected; set => _firstPageWindow.isCustomSeedSelected = value; }
        public static int Seed { get => _firstPageWindow.seed; set => _firstPageWindow.seed = value; }
        public static int InitCapitIc { get => _firstPageWindow.initCapitIc; set => _firstPageWindow.initCapitIc = value; }
        public static bool IsDebug { get => _firstPageWindow.isDebug; set => _firstPageWindow.isDebug = value; }
        public static int MinIqRange { get => _firstPageWindow.minIqRange; set => _firstPageWindow.minIqRange = value; }
        public static int MaxIqRange { get => _firstPageWindow.maxIqRange; set => _firstPageWindow.maxIqRange = value; }
        public static double PHS1 { get => _firstPageWindow.pHS1; set => _firstPageWindow.pHS1 = value; }
        public static double PHS2 { get => _firstPageWindow.pHS2; set => _firstPageWindow.pHS2 = value; }
        public static double PHS3 { get => _firstPageWindow.pHS3; set => _firstPageWindow.pHS3 = value; }
        public static double PIll1 { get => _firstPageWindow.pIll1; set => _firstPageWindow.pIll1 = value; }
        public static double PIll2 { get => _firstPageWindow.pIll2; set => _firstPageWindow.pIll2 = value; }
        public static double PIll3 { get => _firstPageWindow.pIll3; set => _firstPageWindow.pIll3 = value; }
        public static int NumberIterSuspB { get => _firstPageWindow.numberIterSuspB; set => _firstPageWindow.numberIterSuspB = value; }
        public static double NumberDecRate { get => _firstPageWindow.numberDecRate; set => _firstPageWindow.numberDecRate = value; }
        public static int IqLeThan { get => _firstPageWindow.iqLeThan; set => _firstPageWindow.iqLeThan = value; }
        public static int IqGtThan { get => _firstPageWindow.iqGtThan; set => _firstPageWindow.iqGtThan = value; }
        public static double PAccB11 { get => _firstPageWindow.pAccB11; set => _firstPageWindow.pAccB11 = value; }
        public static double PAccB12 { get => _firstPageWindow.pAccB12; set => _firstPageWindow.pAccB12 = value; }
        public static double PAccB13 { get => _firstPageWindow.pAccB13; set => _firstPageWindow.pAccB13 = value; }
        public static double PAccB21 { get => _firstPageWindow.pAccB21; set => _firstPageWindow.pAccB21 = value; }
        public static double PAccB22 { get => _firstPageWindow.pAccB22; set => _firstPageWindow.pAccB22 = value; }
        public static double PAccB23 { get => _firstPageWindow.pAccB23; set => _firstPageWindow.pAccB23 = value; }
        public static double PAccB31 { get => _firstPageWindow.pAccB31; set => _firstPageWindow.pAccB31 = value; }
        public static double PAccB32 { get => _firstPageWindow.pAccB32; set => _firstPageWindow.pAccB32 = value; }
        public static double PAccB33 { get => _firstPageWindow.pAccB33; set => _firstPageWindow.pAccB33 = value; }


        public static double PMobility1 { get => _firstPageWindow.pmob1; set => _firstPageWindow.pmob1 = value; }
        public static double PMobility2 { get => _firstPageWindow.pmob2; set => _firstPageWindow.pmob2 = value; }
        public static double PMobility3 { get => _firstPageWindow.pmob3; set => _firstPageWindow.pmob3 = value; }

        public static double ThrB1Val { get => _firstPageWindow.thrB1_val; set => _firstPageWindow.thrB1_val = value; }
        public static double ThrB2Val { get => _firstPageWindow.thrB2_val; set => _firstPageWindow.thrB2_val = value; }
        public static double ThrB3Val { get => _firstPageWindow.thrB3_val; set => _firstPageWindow.thrB3_val = value; }
        public static double InvAB1Val { get => _firstPageWindow.inv_a_B1_val; set => _firstPageWindow.inv_a_B1_val = value; }
        public static double InvAB2Val { get => _firstPageWindow.inv_a_B2_val; set => _firstPageWindow.inv_a_B2_val = value; }
        public static double InvAB3Val { get => _firstPageWindow.inv_a_B3_val; set => _firstPageWindow.inv_a_B3_val = value; }
        public static int IncreaseOfGapB1 { get => _firstPageWindow.increase_of_GapB1; set => _firstPageWindow.increase_of_GapB1 = value; }
        public static int IncreaseOfGapB2 { get => _firstPageWindow.increase_of_GapB2; set => _firstPageWindow.increase_of_GapB2 = value; }
        public static int IncreaseOfGapB3 { get => _firstPageWindow.increase_of_GapB3; set => _firstPageWindow.increase_of_GapB3 = value; }
        public static double PRiskB1 { get => _firstPageWindow.pRiskB1; set => _firstPageWindow.pRiskB1 = value; }
        public static double PRiskB2 { get => _firstPageWindow.pRiskB2; set => _firstPageWindow.pRiskB2 = value; }
        public static double PRiskB3 { get => _firstPageWindow.pRiskB3; set => _firstPageWindow.pRiskB3 = value; }
        public static double PAvailB1 { get => _firstPageWindow.pAvailB1; set => _firstPageWindow.pAvailB1 = value; }
        public static double PAvailB2 { get => _firstPageWindow.pAvailB2; set => _firstPageWindow.pAvailB2 = value; }
        public static double PAvailB3 { get => _firstPageWindow.pAvailB3; set => _firstPageWindow.pAvailB3 = value; }
        public static int WealthPooper { get => _firstPageWindow.wealth_pooper; set => _firstPageWindow.wealth_pooper = value; }
        public static int WealthFeasible { get => _firstPageWindow.wealth_feasible; set => _firstPageWindow.wealth_feasible = value; }
        public static int WealthRich { get => _firstPageWindow.wealth_rich; set => _firstPageWindow.wealth_rich = value; }
        public static List<RectangleModel> RectList => _firstPageWindow.rectList;
    }
}
