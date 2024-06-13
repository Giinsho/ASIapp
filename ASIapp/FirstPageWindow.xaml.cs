using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using ASIapp.Classes;
using ASIapp.Classes.Agent;
using ASIapp.Classes.Businesses;
using System;

namespace ASIapp
{

    using static Util;
    /// <summary>
    /// Interaction logic for FirstPageWindow.xaml
    /// </summary>
    public partial class FirstPageWindow : UserControl
    {


        public MainWindow _mainWindow;
        string currentDirectory = Directory.GetCurrentDirectory();

        #region Additional data

        private List<Agent> agents = new List<Agent>();
        private List<Business> businesses = new List<Business>();
        private List<Disease> diseases = new List<Disease>();
   


        #endregion

        #region First Section

        public int numberOfRows;
        public int numberOfColumns;

        public int numberOfA;
        public int numberOfD;
        public int numberOfB;

        public string caStateFile;
        public string aProfileFile;
        public string randNumFile;

        public int numberOfIterations;
        public int numberOfExperience;

        public bool isTest1Selected;
        public bool isTest2Selected;
        public bool isTest3Selected;


        public bool isClockSeedSelected;
        public bool isCustomSeedSelected;

        public int seed;

        public int initCapitIc;

        public bool isDebug;

        #endregion

        #region Parameters of A

        public int minIqRange;
        public int maxIqRange;

        #endregion

        #region Health State

        public double pHS1;
        public double pHS2;
        public double pHS3;

        public double pIll1;
        public double pIll2;
        public double pIll3;

        public int numberIterSuspB;
        public double numberDecRate; 

        #endregion

        #region Risk accept level (IQ col)

        public int iqLeThan;
        public int iqGtThan;

        public double pAccB11;
        public double pAccB12;
        public double pAccB13;

        public double pAccB21;
        public double pAccB22;
        public double pAccB23;

        public double pAccB31;
        public double pAccB32;
        public double pAccB33;

        #endregion

        public double pmob1;
        public double pmob2;
        public double pmob3;

        #region Mobility (IQ - col )

        public double thrB1_val;
        public double thrB2_val;
        public double thrB3_val;

        public double inv_a_B1_val;
        public double inv_a_B2_val;
        public double inv_a_B3_val;

        public int increase_of_GapB1;
        public int increase_of_GapB2;
        public int increase_of_GapB3;

        public double pRiskB1;
        public double pRiskB2;
        public double pRiskB3;

        public double pAvailB1;
        public double pAvailB2;
        public double pAvailB3;

        #endregion

        #region Wealth thr

        public int wealth_pooper;
        public int wealth_feasible;
        public int wealth_rich;

        #endregion

        #region Rectangle and mesh
        public List<RectangleModel> rectList;
        #endregion

        public FirstPageWindow(MainWindow window)
        {
            InitializeComponent();
          
            _mainWindow = window;
            rectList = new List<RectangleModel>();
            Initialize(this);

        }

        public void DrawMesh()
        {
            rectList.Clear();
            myCanvas.Children.Clear();
            var cols = numberOfColumns+2;
            var rows = numberOfRows+2;
            
            double cellWidth = myCanvas.ActualWidth / cols;
            double cellHeight = myCanvas.ActualHeight / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Rectangle rect;
                    if (i == 0 || j == 0 || j==cols-1 || i==rows-1 )
                    {
                        rect = new Rectangle
                        {
                            Width = cellWidth,
                            Height = cellHeight,
                            Stroke = Brushes.Red,
                            StrokeThickness = 0.5,
                            Fill = Brushes.Gray

                        };
                
                    } else {
                        rect = new Rectangle
                        {
                            Width = cellWidth,
                            Height = cellHeight,
                            Stroke = Brushes.Black,
                            StrokeThickness = 0.5

                        };
                        rectList.Add(new RectangleModel(Width, Height, i, j, rect));
                    }
                    Canvas.SetLeft(rect, j * cellWidth);
                    Canvas.SetTop(rect, i * cellHeight);
                    myCanvas.Children.Add(rect);


                }
            }

            ColorCellsAsDiseases();
            ColorCellsAsBusinesses();
            ColorCellsAsAgents();
        }

        private void ColorCellsAsDiseases()
        {
            diseases.ForEach(x =>
            {
                var a = rectList.ElementAt(x.GLOBAL_ID - 1);
                a.CellObject.Add(x);
                a.Rectangle.Fill = Brushes.Violet;
            });
        }

        private void ColorCellsAsBusinesses()
        {
            businesses.ForEach(x =>
            {
                var a = rectList.ElementAt(x.GLOBAL_ID - 1);
                a.CellObject.Add(x);
                Brush brush = null;
                switch (x.Type)
                {
                    case Business.B_TYPE.Business1: brush = Brushes.LightBlue; break;
                    case Business.B_TYPE.Business2: brush = Brushes.Blue; break;
                    case Business.B_TYPE.Business3: brush = Brushes.DarkBlue; break;
                }
                a.Rectangle.Fill = brush;
            });
        }

        public void ColorCellsAsAgents()
        {
            agents.ForEach(x =>
            {
                var a = rectList.ElementAt(x.GLOBAL_ID - 1);
                a.CellObject.Add(x);
                Brush brush = null;
                if (x.IsAgentPoor())
                {
                    brush = Brushes.Yellow;
                }
                else if (x.IsAgentFair())
                {
                    brush = Brushes.Orange;
                }
                else if (x.IsAgentRich())
                {
                    brush = Brushes.Red;
                }
                a.Rectangle.Fill = brush;
            });
        }

        public void UpdateMesh()
        {
            
            DrawMesh();
        }

        public void TextBox_PreviewNumbersOnly(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) &&
                e.Text != "." &&
                e.Text != "-" ||
                (e.Text == "-" && ((TextBox)sender).Text.Length > 0) ||
                ((TextBox)sender).Text.Contains(".") && e.Text == ".")
            {
                e.Handled = true;
            }
        }

        public void Btn_run_Click(object sender, RoutedEventArgs e)
        {
            RandomGen = isCustomSeedSelected == true ? new Random(seed) : new Random();
       
            insertAgents();
            insertBusinesses();
            insertDiseases();
            UpdateMesh();
        }

        private void insertObject<T>(List<T> cells, int numberOf, Func<int, int, bool> freeFunc, Func<int, int, int, T> addFunc) where T : CellObject
        {
            cells.Clear();
            for (int i = 1; i <= numberOf; i++)
            {
                bool freeSpace = false;
                var randCol = 0;
                var randRow = 0;
                while (!freeSpace)
                {
                    randCol = RandomGen.Next(1, numberOfColumns);
                    randRow = RandomGen.Next(1, numberOfRows);
                    freeSpace = freeFunc(randCol, randRow);
                }
                var x = addFunc(i, randCol, randRow);
                cells.Add(x);
            }
        }

        private void insertDiseases()
        {
            insertObject(diseases, numberOfD, (randCol, randRow) =>
            {
                var anyAgent = !agents.Any(x => x.DecalculateGlobalID(numberOfColumns, numberOfRows).Equals(new System.Drawing.Point(randCol, randRow)));
                var anyBusiness = !businesses.Any(x => x.DecalculateGlobalID(numberOfColumns, numberOfRows).Equals(new System.Drawing.Point(randCol, randRow)));
                return anyAgent && anyBusiness;
            }, (i, randCol, randRow) =>
            {
                return new Disease() { ID = i, GLOBAL_ID = Agent.CalculateGlobalID(numberOfColumns, randCol, randRow) };
            });
        }

        private void insertBusinesses()
        {
            insertObject(businesses, numberOfB, (randCol, randRow) =>
            {
                return !agents.Any(x => x.DecalculateGlobalID(numberOfColumns, numberOfRows).Equals(new System.Drawing.Point(randCol, randRow)));
            }, (i, randCol, randRow) =>
            {
                var randomType = (Business.B_TYPE)RandomGen.Next(0, Enum.GetNames(typeof(Business.B_TYPE)).Length);
                var business = new Business(randomType) { ID = i, GLOBAL_ID = Agent.CalculateGlobalID(numberOfColumns, randCol, randRow) };
                return business;
            });
        }

        private void insertAgents()
        {
            insertObject(agents, numberOfA, (randCol, randRow) =>
            {
                return !agents.Any(x => x.DecalculateGlobalID(numberOfColumns, numberOfRows).Equals(new System.Drawing.Point(randCol, randRow)));
            }, (i, randCol, randRow) =>
            {
                var agent = new Agent() { ID = i, GLOBAL_ID = Agent.CalculateGlobalID(numberOfColumns, randCol, randRow), };
                agent.CAPITAL = initCapitIc;
                agent.INIT_CAPITAL = initCapitIc;
                return agent;
            });
        }

        static string GetFileNameFromPath(string filePath)
        {
            return System.IO.Path.GetFileName(filePath);
        }

        public void Btn_readCaStates_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    caStateFile = GetFileNameFromPath(fileName + "/CA_STATES");
                    string[] fileLines = File.ReadAllLines(fileName);
                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        Console.WriteLine(fileLines[i]);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}");
                }
            }
            UpdateRadioButtonStatus(rbtn_readCaStates, true);
        }

        public void Btn_readAProfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    aProfileFile = GetFileNameFromPath(fileName + "/A_PROFILE");
                    string[] fileLines = File.ReadAllLines(fileName);
                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        Console.WriteLine(fileLines[i]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}");
                }
            }
            UpdateRadioButtonStatus(rbtn_readAprofile, true);

        }

        public void Btn_ReadRandNum_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    randNumFile = GetFileNameFromPath(fileName + "/RAND_NUM");
                    string[] fileLines = File.ReadAllLines(fileName);
                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        Console.WriteLine(fileLines[i]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}");
                }
            }
            UpdateRadioButtonStatus(rbtn_readRandNum, true);

        }


        public void NumberOfRows_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfRows);
        }

        public void NumberOfColumns_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfColumns);
        }

        public void NumberOfA_TextChanged(object sender, TextChangedEventArgs e)
        {

            UpdateParameterInt(sender, ref numberOfA);
        }

        public void NumberOfD_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfD);

        }

        public void NumberOfB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfB);

        }

        public void NumberOfiter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfIterations);

        }

        public void NumberOfexper_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberOfExperience);
        }

        public void UpdateRadioButtonStatus(RadioButton radioButton, bool isSuccess)
        {
            if (radioButton != null)
            {
                radioButton.IsChecked = isSuccess;
            }
        }

        public void CheckBoxDebug_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            isDebug = checkBox.IsChecked ?? false;

        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            // Deselect all other RadioButtons
            if (radioButton == test_1)
            {
                isTest1Selected = true;
                isTest2Selected = false;
                isTest3Selected = false;
            }
            else if (radioButton == test_2)
            {
                isTest1Selected = false;
                isTest2Selected = true;
                isTest3Selected = false;
            }
            else if (radioButton == test_3)
            {
                isTest1Selected = false;
                isTest2Selected = false;
                isTest3Selected = true;
            }
            // Ensure that only one RadioButton is checked at a time
            else
            {
                test_1.IsChecked = false;
                test_2.IsChecked = false;
                test_3.IsChecked = false;
            }
        }

        public void RbtnCustomSeed_Checked(object sender, RoutedEventArgs e)
        {

            CustomSeedText.IsEnabled = true;


            RbtnClockSeed.IsChecked = false;


        }

        public void RbtnClockSeed_Checked(object sender, RoutedEventArgs e)
        {

            CustomSeedText.IsEnabled = false;


            CustomSeedText.Clear();
        }

        public void CustomSeedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref seed);
        }

        public void InitCapitIC_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateParameterInt(sender, ref initCapitIc);

        }

        public void paccB11_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB11);
        }

        public void paccB12_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB12);
        }

        public void paccB13_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB13);
        }

        public void paccB21_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB21);
        }

        public void paccB22_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB22);
        }

        public void paccB23_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB23);
        }

        public void paccB31_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB31);
        }

        public void paccB32_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB32);
        }

        public void paccB33_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB33);
        }

        public void UpdateParameterDouble(object sender, ref double parameter)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                if (double.TryParse(textBox.Text, out double newValue))
                {
                    parameter = newValue;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        public void UpdateParameterInt(object sender, ref int parameter)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                if (int.TryParse(textBox.Text, out int newValue))
                {
                    parameter = newValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }


        public void numberDicDecRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref numberDecRate);
        }

        public void numberOfIterSuspB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberIterSuspB);
        }


        public void iqLevelGreaterThan_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref iqGtThan);
        }

        public void iqLevelLessThan_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref iqLeThan);
        }

        public void thrB1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB1_val);
        }

        public void thrB2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB2_val);
        }

        public void thrB3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB3_val);
        }

        public void inv_a_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B1_val);
        }

        public void inv_a_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B2_val);
        }

        public void inv_a_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B3_val);
        }

        public void increase_of_gap_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB1);
        }

        public void increase_of_gap_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB2);
        }

        public void increase_of_gap_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB3);
        }

        public void p_risk_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pRiskB1);
        }

        public void p_risk_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pRiskB2);
        }

        public void p_risk_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pRiskB3);
        }

        public void p_avail_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAvailB1);
        }

        public void p_avail_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAvailB2);
        }

        public void p_avail_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAvailB3);
        }


        public void pmob1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pmob1);
        }

        public void pmob2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pmob2);
        }


        public void pmob3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pmob3);
        }


        public void thr_pooper_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_pooper);
        }

        public void thr_rich_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_feasible);
        }

        public void thr_feasible_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_rich);
        }

        public void probabilityHS1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pHS1);
        }

        public void probabilityHS2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pHS2);
        }

        public void probabilityHS3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pHS3);
        }

        public void probabilityIll1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pIll1);
        }

        public void probabilityIll2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pIll2);
        }

        public void probabilityIll3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pIll3);
        }

        public void iqMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref maxIqRange);
        }

        public void iqMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref minIqRange);
        }
    }
}
