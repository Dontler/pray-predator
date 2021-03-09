using System.Collections.Generic;
using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using PrayPredator.lib.algorithms;

namespace PrayPredator.lib
{
    public class LotkaVolterraGraphic : IGraphic
    {
        private readonly Config _config;
        private ChartValues<double> Abscissa { get; }
        private ChartValues<double> Ordinates { get; }
        private ChartValues<ObservablePoint> Points { get; }

        public LotkaVolterraGraphic(Config config, ChartValues<double> abscissa, ChartValues<double> ordinates, ChartValues<ObservablePoint> points)
        {
            _config = config;
            Abscissa = abscissa;
            Ordinates = ordinates;
            Points = points;
        }

        public void Update(IAlgorithm algorithm, Conditions conditions)
        {
            var points = CalculatePoints(algorithm, conditions);
            foreach (var point in points)
            {
                Abscissa.Add(point.X);
                Ordinates.Add(point.Y);
            }
        }

        public void UpdatePhase(IAlgorithm algorithm, Conditions conditions)
        {
            var points = CalculatePoints(algorithm, conditions);
            foreach (var point in points)
            {
                Points.Add(new ObservablePoint(point.X, point.Y));
            }
        }

        private IEnumerable<Point> CalculatePoints(IAlgorithm algorithm, Conditions conditions)
        {
            ClearState();
            var points = new List<Point>();
            
            var dX = _config.InitX;
            var dY = _config.InitY;

            double XFunc(double y, double x) => (conditions.Alpha - conditions.Beta * y) * x;
            double YFunc(double x, double y) => (-conditions.Gamma + conditions.Delta * x) * y;

            for (int i = 0; i < _config.IterationsCount; i++)
            {
                var x = algorithm.Evaluate(dY, dX, _config.Step, XFunc);
                var y = algorithm.Evaluate(dX, dY, _config.Step, YFunc);

                dX = x;
                dY = y;

                if (i % _config.DrawStep != 0) continue;
                points.Add(new Point(dX, dY));
            }

            return points;
        }

        private void ClearState()
        {
            Abscissa.Clear();
            Ordinates.Clear();
            Points.Clear();   
        }
    }
}