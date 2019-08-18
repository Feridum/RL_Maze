using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleExperiment : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool isAStarExperiment = false;

    [SerializeField]
    private GameObject RLController;
    void Start()
    {
        GameObject instance = new GameObject();

        if (isAStarExperiment)
        {

        }
        else
        {
            instance = Instantiate(RLController, transform.position, Quaternion.identity);
            
        }

        instance.transform.SetParent(transform.parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
