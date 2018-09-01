using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridObjects : MonoBehaviour {

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameObject finishObject;

    GameManager gameManager;
    Translation translation;
    private Vector2 gridSize;
    void Start () {
        this.gameManager = GameManager.gameManager;
        translation = gameManager.getTranslationForGrid("main");
        gridSize = gameManager.getGridSize();
        this.createPlayer();
        this.createFinish();
    }

    private void createPlayer()
    {
        Vector3 position = new Vector3(translation.getXPosition(0), translation.getYPosition(0), -1);
        Instantiate(playerObject, position, Quaternion.identity);
    }

    private void createFinish()
    {
        Vector3 position = new Vector3(translation.getXPosition((int)gridSize.x - 1), translation.getYPosition((int)gridSize.y - 1), -1);
        Instantiate(finishObject, position, Quaternion.identity);
    }
}
