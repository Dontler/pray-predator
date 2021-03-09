using PrayPredator.lib.algorithms;

namespace PrayPredator.lib
{
    public interface IGraphic
    {
        public void Update(IAlgorithm algorithm, Conditions conditions);
        public void UpdatePhase(IAlgorithm algorithm, Conditions conditions);
    }
}