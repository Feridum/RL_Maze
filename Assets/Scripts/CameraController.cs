using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	void Start () {
        Camera cam = Camera.main;
        GridManager gridManager = GridManager.gridManager;
        Vector2 gridSize = gridManager.getGridSize();

        cam.orthographicSize = gridSize.x > gridSize.y ? gridSize.x : gridSize.y;
    }
}
