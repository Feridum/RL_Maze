using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnClick : MonoBehaviour {
    private void OnMouseUp()
    {
        GameManager.gameManager.removeObstacleFromMaze(this.transform.position);
        Destroy(gameObject);
    }
}
