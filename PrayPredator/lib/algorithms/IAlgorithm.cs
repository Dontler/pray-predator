using System;

namespace PrayPredator.lib.algorithms
{
    public interface IAlgorithm
    {
        public double Evaluate(double x, double y, double h, Func<double, double, double> f);
    }
}