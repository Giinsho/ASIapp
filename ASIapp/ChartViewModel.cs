using ASIapp.Classes.Agent;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
namespace ASIapp
{
    using static Util;
    public class ChartViewModel
    {

        public ISeries[] Series { get; set; }
        public LabelVisual Title { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public ChartViewModel()
        {
            InitializeChart();
           
        }

        public void InitializeChart()
        {
            // Define chart series
            List<string> titles = new List<string> { "Poor", "Fair", "Rich", "Init Capital" };
            List<SKColor> colors = new List<SKColor>
            {
                SKColors.Blue,
                SKColors.Green,
                SKColors.Red,
                SKColors.Black
            };

            // Initialize chart series
            List<ISeries> chartSeries = new List<ISeries>();

            for (int i = 0; i < titles.Count; i++)
            {
                chartSeries.Add(new LineSeries<double>
                {
                    Name = titles[i], // Updated to match the current LiveChartsCore property
                    Values = new double[] { InitCapitIc }, // Example data
                    Stroke = new SolidColorPaint(colors[i]),
                    Fill = null
                });
            }

            Series = chartSeries.ToArray();

            Title = new LabelVisual
            {
                Text = "Agents",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Iteration",
                    LabelsRotation = 15,
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightGray),
                    TextSize = 20
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Wealth",
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightGray),
                    TextSize = 20
                }
            };
        }

        public void UpdateSeriesValues(IEnumerable<double> richValues, IEnumerable<double> fairValues, IEnumerable<double> poorValues)
        {
            if (Series.Length >= 3)
            {
                var poorSeries = Series[0] as LineSeries<double>;
                var fairSeries = Series[1] as LineSeries<double>;
                var richSeries = Series[2] as LineSeries<double>;

                if (poorSeries != null)
                {
                    poorSeries.Values = poorValues.ToArray();
                }
                if (fairSeries != null)
                {
                    fairSeries.Values = fairValues.ToArray();
                }
                if (richSeries != null)
                {
                    richSeries.Values = richValues.ToArray();
                }
            }
        }
    }
}
