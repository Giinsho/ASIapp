﻿using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASIapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FirstPageWindow FirstPageWindow;
        public MainWindow()
        {
            InitializeComponent();
            CreateFolders();

            FirstPageWindow = new FirstPageWindow(this);

            ViewControl.Content = FirstPageWindow.Content;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        }

        private void CreateFolders()
        {
            if (!Directory.Exists("CA_STATES"))
            {
                Directory.CreateDirectory("CA_STATES");
            }
            if (!Directory.Exists("A_PROFILE"))
            {
                Directory.CreateDirectory("A_PROFILE");
            }

            if (!Directory.Exists("RAND_NUM"))
            {
                Directory.CreateDirectory("RAND_NUM");
            }

            if (!Directory.Exists("RESULTS"))
            {
                Directory.CreateDirectory("RESULTS");
            }

            if (!Directory.Exists("RESULTS/DEBUG"))
            {
                Directory.CreateDirectory("RESULTS/DEBUG");
            }





        }



    }


}