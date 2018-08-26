using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    // Use this for initialization
    Player player;
    
    public void getNextMoveDirection()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            player.moveUp();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            player.moveDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            player.moveLeft();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            player.moveRight();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            player.placeOnRandomPosition(false);
        }
    }

    void Start () {
        player = transform.parent.gameObject.GetComponent<Player>();
        float[,] test = new float[,] { { 1, 2}, { 3, 4} };

        FileReader file = new FileReader();
        //file.saveToFile(test);
        file.readFromFile();
	}

    void Update()
    {
        this.getNextMoveDirection();
    }
}
