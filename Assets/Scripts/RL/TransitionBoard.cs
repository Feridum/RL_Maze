using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        GameManager daBoss = (GameManager)FindObjectOfType(typeof(GameManager));
        Gizmos.DrawWireCube(transform.position, daBoss.getGridSize());
    }
}
