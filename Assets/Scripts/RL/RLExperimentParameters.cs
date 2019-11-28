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

        int numberOfIterations = 20;

        int currentNumberOfFinishedEpoches = 0;
        int currentNumberOfFIterations = 0;

        public RLExperimentParameters(float futureStepsRate, float learningRate, int numberOfEpoches)
        {
            this.futureStepsRate = futureStepsRate;
            this.learningRate = learningRate;
            this.numberOfEpoches = numberOfEpoches;
        }

        public bool shouldStartNewEpoche()
        {
            currentNumberOfFinishedEpoches = currentNumberOfFinishedEpoches + 1;

            if(currentNumberOfFinishedEpoches < numberOfEpoches)
            {
                currentNumberOfFIterations = 0;
            }

            return currentNumberOfFinishedEpoches < numberOfEpoches;
        }

        public bool shouldStartNewIteration(List<DataRow> dataRows)
        {
            currentNumberOfFIterations = currentNumberOfFIterations + 1;

            DataRow[] reduced = dataRows.Skip(Math.Max(0, dataRows.Count() - 3)).ToArray();

            return reduced.Length < 3 || !(reduced[0].steps == reduced[1].steps && reduced[1].steps == reduced[2].steps);
        }

        public int getCurrentEpoche()
        {
            return currentNumberOfFinishedEpoches;
        }

        public int getCurrentIteration()
        {
            return currentNumberOfFIterations;
        }
    }
}
