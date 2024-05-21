using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace ASIapp
{
    /// <summary>
    /// Interaction logic for FirstPageWindow.xaml
    /// </summary>
    public partial class FirstPageWindow : UserControl
    {
        private MainWindow _mainWindow;
        string currentDirectory = Directory.GetCurrentDirectory();

        #region First Section

        private int numberOfRows;
        private int numberOfColumns;

        private int numberOfA;
        private int numberOfD;
        private int numberOfB;

        private string caStateFile;
        private string aProfileFile;
        private string randNumFile;

        private int numberOfIterations;
        private int numberOfExperience;

        private bool isTest1Selected;
        private bool isTest2Selected;
        private bool isTest3Selected;


        private bool isClockSeedSelected;
        private bool isCustomSeedSelected;

        private int seed;

        private int initCapitIc;

        private bool isDebug;

        #endregion

        #region Parameters of A

        private int minIqRange;
        private int maxIqRange;

        #endregion

        #region Health State

        private int pHS1;
        private int pHS2;
        private int pHS3;

        private int pIll1;
        private int pIll2;
        private int pIll3;

        private int numberIterSuspB;
        private double numberDecRate; 

        #endregion

        #region Risk accept level (IQ col)

        private int iqLeThan;
        private int iqGtThan;

        private double pAccB11;
        private double pAccB12;
        private double pAccB13;

        private double pAccB21;
        private double pAccB22;
        private double pAccB23;

        private double pAccB31;
        private double pAccB32;
        private double pAccB33;

        #endregion

        #region Mobility (IQ - col )

        private double thrB1_val;
        private double thrB2_val;
        private double thrB3_val;

        private double inv_a_B1_val;
        private double inv_a_B2_val;
        private double inv_a_B3_val;

        private int increase_of_GapB1;
        private int increase_of_GapB2;
        private int increase_of_GapB3;

        private double pRiskB1;
        private double pRiskB2;
        private double pRiskB3;

        private double pAvailB1;
        private double pAvailB2;
        private double pAvailB3;

        #endregion

        #region Wealth thr

        private int wealth_pooper;
        private int wealth_feasible;
        private int wealth_rich;

        #endregion

        #region Rectangle and mesh
        List<RectangleModel> rectList;
        #endregion

        public FirstPageWindow(MainWindow window)
        {
            InitializeComponent();

            _mainWindow = window;
            rectList = new List<RectangleModel>();

        }

        private void DrawMesh()
        {
            rectList.Clear();
            myCanvas.Children.Clear();
            var cols = numberOfColumns+2;
            var rows = numberOfRows+2;
            
            double cellWidth = myCanvas.ActualWidth / cols;
            double cellHeight = myCanvas.ActualHeight / rows;

            for (int i = 0; i <= rows; i++)
            {
                for (int j = 0; j <= cols; j++)
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

            ColorCellsAsAgents();

        }

        private void ColorCellsAsAgents()
        {
            var random = new Random();
            var numberOfAgents = numberOfA;

            rectList = rectList.OrderBy(x => random.Next()).ToList();

            foreach (var rect in rectList.Take(numberOfAgents))
            {
                rect.Rectangle.Fill = Brushes.Orange;
            }
        }

        private void UpdateMesh()
        {
            
            DrawMesh();
        }

        private void TextBox_PreviewNumbersOnly(object sender, TextCompositionEventArgs e)
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

        private void NumberOfRows_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfRows.Text, out int newValue);
                numberOfRows = newValue;
          
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        private void NumberOfColumns_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfColumns.Text, out int newValue);
                numberOfColumns = newValue;
           
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        private void NumberOfA_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                int.TryParse(NumberOfA.Text, out int newValue);
                numberOfA= newValue;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        private void NumberOfD_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfD.Text, out int newValue);
                numberOfD = newValue;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }

        }

        private void NumberOfB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfB.Text, out int newValue);
                numberOfB = newValue;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }

        }

        private void NumberOfiter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfiter.Text, out int newValue);
                numberOfIterations = newValue;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }

        }

        private void NumberOfexper_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int.TryParse(NumberOfexper.Text, out int newValue);
                numberOfExperience = newValue;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }

        }

        private void UpdateRadioButtonStatus(RadioButton radioButton, bool isSuccess)
        {
            if (radioButton != null)
            {
                radioButton.IsChecked = isSuccess;
            }
        }

        static string GetFileNameFromPath(string filePath)
        {
            return System.IO.Path.GetFileName(filePath);
        }

        private void Btn_readCaStates_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    caStateFile = GetFileNameFromPath(fileName);
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
            UpdateRadioButtonStatus(rbtn_readCaStates,true);
        }

        private void Btn_readAProfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    aProfileFile = GetFileNameFromPath(fileName);
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

        private void Btn_ReadRandNum_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "");

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    randNumFile = GetFileNameFromPath(fileName);
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

        private void CheckBoxDebug_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            isDebug = checkBox.IsChecked ?? false; 

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
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

        private void RbtnCustomSeed_Checked(object sender, RoutedEventArgs e)
        {

            CustomSeedText.IsEnabled = true;

   
            RbtnClockSeed.IsChecked = false;

            
        }

        private void RbtnClockSeed_Checked(object sender, RoutedEventArgs e)
        {
       
            CustomSeedText.IsEnabled = false;

         
            CustomSeedText.Clear();
        }

        private void CustomSeedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref seed);
        }

        private void InitCapitIC_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateParameterInt(sender, ref initCapitIc);

        }

        private void paccB11_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB11);
        }

        private void paccB12_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB12);
        }

        private void paccB13_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB13);
        }

        private void paccB21_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB21);
        }

        private void paccB22_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB22);
        }

        private void paccB23_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB23);
        }

        private void paccB31_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB31);
        }

        private void paccB32_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB32);
        }

        private void paccB33_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAccB33);
        }

        private void UpdateParameterDouble(object sender, ref double parameter)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                if (double.TryParse(textBox.Text, out double newValue))
                {
                    parameter = newValue;
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        private void UpdateParameterInt(object sender, ref int parameter)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                if (int.TryParse(textBox.Text, out int newValue))
                {
                    parameter = newValue;
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid number.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing value: {ex.Message}");
            }
        }

        private void iqLevelGreaterThan_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref iqGtThan);
        }

        private void iqLevelLessThan_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref iqLeThan);
        }

        private void thrB1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB1_val);
        }

        private void thrB2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB2_val);
        }

        private void thrB3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref thrB3_val);
        }

        private void inv_a_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B1_val);
        }

        private void inv_a_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B2_val);
        }

        private void inv_a_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref inv_a_B3_val);
        }

        private void increase_of_gap_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB1);
        }

        private void increase_of_gap_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB2);
        }

        private void increase_of_gap_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref increase_of_GapB3);
        }

        private void p_risk_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender,ref pRiskB1);
        }

        private void p_risk_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender,ref pRiskB2);
        }

        private void p_risk_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender,ref pRiskB3);
        }

        private void p_avail_B1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender,ref pAvailB1);
        }

        private void p_avail_B2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender,ref pAvailB2);
        }

        private void p_avail_B3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref pAvailB3);
        }

        private void thr_pooper_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_pooper);
        }

        private void thr_rich_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_feasible);
        }

        private void thr_feasible_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref wealth_rich);
        }

        private void probabilityHS1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pHS1);
        }

        private void probabilityHS2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pHS2);
        }

        private void probabilityHS3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pHS3);
        }

        private void probabilityIll1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pIll1);
        }

        private void probabilityIll2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pIll2);
        }

        private void probabilityIll3_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref pIll3);
        }

        private void iqMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref maxIqRange);
        }

        private void iqMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref minIqRange);
        }

        private void Btn_run_Click(object sender, RoutedEventArgs e)
        {
           UpdateMesh();
        }

        private void numberDicDecRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterDouble(sender, ref numberDecRate);
        }

        private void numberOfIterSuspB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateParameterInt(sender, ref numberIterSuspB);
        }
    }
}
