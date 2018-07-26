using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, FindPathInterface {

    // Use this for initialization
    Player playerMovement;

    public void getNextMoveDirection()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerMovement.moveUp();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerMovement.moveDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerMovement.moveLeft();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerMovement.moveRight();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            playerMovement.resetPosition();
        }
    }

    void Start () {
        playerMovement = transform.parent.gameObject.GetComponent<Player>();
	}

    void Update()
    {
        this.getNextMoveDirection();
    }
}
