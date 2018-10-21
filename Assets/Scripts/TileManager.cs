using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    [SerializeField]
    private GameObject wallObject;

    Player player;

    // Use this for initialization
    void Start () {
        calculateFloorDimesions();
        player = Object.FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CreateWall(new Vector3(this.transform.position.x, this.transform.position.y, -1));
        }
        else if (Input.GetMouseButtonUp(1))
        {
            player.placeOnPosition(new Vector3(this.transform.position.x, this.transform.position.y, -1));
        }
       
    }


    private void CreateWall(Vector3 position)
    {
        GameManager.gameManager.addObstacleToMaze(position);
        Instantiate(wallObject, position, Quaternion.identity);
    }

    private void calculateFloorDimesions()
    {
        wallObject.transform.localScale = this.transform.localScale;
    }
}
