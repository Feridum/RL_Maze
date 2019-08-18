using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.RL
{
    class RLExperimentParameters
    {
        float futureStepsRate { get; } = 0.6f;
        float learningRate { get; } = 1.0f;
        int numberOfEpoches { get; } = 2;

        int currentNumberOfFinishedEpoches = 0;
        public RLExperimentParameters(float futureStepsRate, float learningRate, int numberOfEpoches)
        {
            this.futureStepsRate = futureStepsRate;
            this.learningRate = learningRate;
            this.numberOfEpoches = numberOfEpoches;
        }

        public bool shouldStartNewEpoche()
        {
            currentNumberOfFinishedEpoches = currentNumberOfFinishedEpoches + 1;

            return currentNumberOfFinishedEpoches < numberOfEpoches;
        }


    }
}
