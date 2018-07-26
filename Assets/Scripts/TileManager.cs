using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    [SerializeField]
    private GameObject wallObject;

	// Use this for initialization
	void Start () {
        calculateFloorDimesions();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {
        CreateWall(new Vector3(this.transform.position.x, this.transform.position.y,-1));
    }


    private void CreateWall(Vector3 position)
    {
        Instantiate(wallObject, position, Quaternion.identity);
    }

    private void calculateFloorDimesions()
    {
        wallObject.transform.localScale = this.transform.localScale;
    }
}
