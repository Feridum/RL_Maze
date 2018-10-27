using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLManager : MonoBehaviour {

    public static RLManager rlManager = null;
    float[,] Q = null;

    void Awake()
    {

        if (rlManager == null)
        {
            rlManager = this;
        }
        else if (rlManager != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    public void updateQ(float[,] Q)
    {
        this.Q = Q;
    }

    public float[,] getQ()
    {
        return Q;
    }
}
