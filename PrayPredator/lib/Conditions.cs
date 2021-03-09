namespace PrayPredator.lib
{
    public class Conditions
    {
        public Conditions(double alpha, double beta, double gamma, double delta)
        {
            Alpha = alpha;
            Beta = beta;
            Gamma = gamma;
            Delta = delta;
        }

        public double Alpha { get; set; }

        public double Beta { get; set; }

        public double Gamma { get; set; }

        public double Delta { get; set; }
    }
}