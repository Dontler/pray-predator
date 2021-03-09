using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LiveCharts;
using LiveCharts.Defaults;
using PrayPredator.lib;
using PrayPredator.lib.algorithms;
using LiveCharts.Wpf;

namespace PrayPredator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGraphic _graphic;
        private readonly Conditions _conditions;
        private Algorithms _algorithmType;

        public SeriesCollection Series { get; set; }
        public Func<double, string> LabelFormatter { get; set; }
        public Func<int, string> TimeFormatter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var config = new Config(1000, 0.04, 10,10, 10);
            _conditions = new Conditions(0.42, 0.14, 0.79, 0.17);
            _algorithmType = Algorithms.RungeKutta;
            
            var prays = new ChartValues<double>();
            var predators = new ChartValues<double>();
            var points = new ChartValues<ObservablePoint>();

            _graphic = new LotkaVolterraGraphic(config, prays, predators, points);

            Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Prays",
                    Values = prays
                },
                new LineSeries
                {
                    Title = "Predators",
                    Values = predators
                },
                new LineSeries
                {
                    Title = "Population",
                    Values = points
                }
            };

            LabelFormatter = value => value.ToString("F1");
            TimeFormatter = value => 
                value == config.IterationsCount ? TimeFormatter(value + config.DrawStep) : value.ToString("F1");

            DataContext = this;

            AlphaSb.Value = _conditions.Alpha;
            BetaSb.Value = _conditions.Beta;
            GammaSb.Value = _conditions.Gamma;
            DeltaSb.Value = _conditions.Delta;
            
            UpdateGraphic();
        }

        private void AlphaSb_OnScroll(object sender, ScrollEventArgs e)
        {
            _conditions.Alpha = e.NewValue;
            UpdateGraphic();
        }
        
        private void BetaSb_OnScroll(object sender, ScrollEventArgs e)
        {
            _conditions.Beta = e.NewValue;
            UpdateGraphic();
        }
        
        private void GammaSb_OnScroll(object sender, ScrollEventArgs e)
        {
            _conditions.Gamma = e.NewValue;
            UpdateGraphic();
        }
        
        private void DeltaSb_OnScroll(object sender, ScrollEventArgs e)
        {
            _conditions.Delta = e.NewValue;
            UpdateGraphic();
        }

        private void ChangeAlgorithm(object sender, RoutedEventArgs e)
        {
            var btn = (RadioButton) sender;
            if (ReferenceEquals(btn.Tag, "Eulers"))
            {
                _algorithmType = Algorithms.Euler;
                return;
            }

            _algorithmType = Algorithms.RungeKutta;
        }

        private void UpdateGraphic()
        {
            try
            {
                switch (_algorithmType)
                {
                    case Algorithms.Euler:
                        if (Phase.IsChecked != null && Phase.IsChecked.Value)
                        {
                            _graphic.UpdatePhase(new EulersAlgorithm(), _conditions);
                            return;
                        }
                        _graphic.Update(new EulersAlgorithm(), _conditions);
                        break;
                    case Algorithms.RungeKutta:
                        if (Phase.IsChecked != null && Phase.IsChecked.Value)
                        {
                            _graphic.UpdatePhase(new RungeKuttaAlgorithm(), _conditions);
                            return;
                        }
                        _graphic.Update(new RungeKuttaAlgorithm(), _conditions);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Reset();
            }
        }

        private void Reset()
        {
            _conditions.Alpha = 0.42;
            _conditions.Beta = 0.14;
            _conditions.Gamma = 0.79;
            _conditions.Delta = 0.17;
        }
    }
}