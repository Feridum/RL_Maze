using Assets.Scripts.RL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLManager : MonoBehaviour {

    public static RLManager rlManager = null;
    float[,] Q = null;
    float[,] R = null;
    private RLExperimentParameters rLExperimentParameters { get; set; }
    private DataCollectionManager dataCollectionManager;

    void Awake()
    {

        if (rlManager == null)
        {
            rlManager = this;
        }
        else if (rlManager != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        rLExperimentParameters = new RLExperimentParameters((float)0.6, (float)1.0, 5);
        dataCollectionManager = new DataCollectionManager();
    }

    // Update is called once per frame
    public void updateQ(float[,] Q)
    {
        this.Q = Q;
    }

    public void updateR(float[,] R)
    {
        this.R = R;
    }

    public float[,] getQ()
    {
        return Q;
    }

    public float[,] getR()
    {
        return R;
    }

    public bool shouldStartNewEpoche()
    {
        return rLExperimentParameters.shouldStartNewEpoche();
    }

    public bool shouldStartNewIteration()
    {
        return rLExperimentParameters.shouldStartNewIteration(dataCollectionManager.getDataRows());
    }

    public void saveIterationInformation(int steps)
    {
        dataCollectionManager.addNewData(new DataRow(rLExperimentParameters.getCurrentEpoche(), rLExperimentParameters.getCurrentIteration(), steps));
    }

    public void saveResults()
    {
        dataCollectionManager.saveResults();
    }
}
