using System;

namespace PrayPredator.lib.algorithms
{
    public class RungeKuttaAlgorithm : IAlgorithm
    {
        public double Evaluate(double x, double y, double h, Func<double, double, double> f)
        {
            var k1 = f(x, y);
            var k2 = f(x + h / 2, y + h * (k1 / 2));
            var k3 = f(x + h / 2, y + h * (k2 / 2));
            var k4 = f(x + h, y + h * k3);

            return y + h / 6 * (k1 + k2 + k3 + k4);
        }
    }
}