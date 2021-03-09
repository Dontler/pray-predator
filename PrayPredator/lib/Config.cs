namespace PrayPredator.lib
{
    public class Config
    {
        public Config(int iterationsCount, double step, int drawStep, double initX, double initY)
        {
            IterationsCount = iterationsCount;
            Step = step;
            DrawStep = drawStep;
            InitX = initX;
            InitY = initY;
        }

        public int IterationsCount { get; }
        public double Step { get; }
        public int DrawStep { get; }
        public double InitX { get; }
        public double InitY { get; }
    }
}