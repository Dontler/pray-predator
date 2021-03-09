using System;
using System.Drawing;

namespace PrayPredator.lib.algorithms
{
    public class EulersAlgorithm : IAlgorithm
    {

        public double Evaluate(double x, double y, double h, Func<double, double, double> f)
        {
            return y + h * f(x, y);
        }
    }
}