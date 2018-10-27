using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera cam = Camera.main;
        GridManager gridManager = GridManager.gridManager;
        Vector2 gridSize = gridManager.getGridSize();

        cam.orthographicSize = gridSize.x > gridSize.y ? gridSize.x : gridSize.y;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
