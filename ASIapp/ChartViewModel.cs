using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;

namespace ASIapp
{
    using static Util;
    public class ChartViewModel : INotifyPropertyChanged
    {
        private SeriesCollection? _series;

        public SeriesCollection? Series
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        private Func<double, string>? _yAxes;

        public Func<double, string>? YAxes
        {
            get { return _yAxes; }
            set
            {
                _yAxes = value;
                OnPropertyChanged(nameof(YAxes));
            }
        }

        private List<string>? _xAxes;

        public List<string>? XAxes
        {
            get { return _xAxes; }
            set
            {
                _xAxes = value;
                OnPropertyChanged(nameof(XAxes));
            }
        }

        public ChartViewModel()
        {
            InitializeChart();

        }

        private void InitializeChart()
        {
            Series = new SeriesCollection();

            // Example data setup (replace with actual logic)
            var richValues = new ChartValues<double> {10};
            var fairValues = new ChartValues<double> {10};
            var poorValues = new ChartValues<double> {10};

            Series.Add(new LineSeries { Title = "Poor", Values = richValues });
            Series.Add(new LineSeries { Title = "Fair", Values = fairValues });
            Series.Add(new LineSeries { Title = "Rich", Values = poorValues });
            Series.Add(new LineSeries { Title = "InitCapital", Values = new ChartValues<double> { 10 } });


            YAxes = value => value.ToString();
        
            
        }

        public void ResetChart()
        {
            for (int i = 0; i < 4; i++)
                Series[i].Values.Clear();
        }

        public void UpdateSeriesValues(ChartValues<double> richValues, ChartValues<double> fairValues, ChartValues<double> poorValues)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Series.Count >= 4)
                {
                    // Find the maximum value in each collection
                    double maxPoorValue = poorValues.Any() ? poorValues.Max() : 0;
                    double maxFairValue = fairValues.Any() ? fairValues.Max() : 0;
                    double maxRichValue = richValues.Any() ? richValues.Max() : 0;

                    // Add the maximum values to the corresponding series
                    Series[0].Values.Add((double)maxPoorValue);
                    Series[1].Values.Add((double)maxFairValue );
                    Series[2].Values.Add((double)maxRichValue );

                    // Add InitCapitIc to the Init Capital series
                    Series[3].Values.Add((double)InitCapitIc);
                }
            });
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}