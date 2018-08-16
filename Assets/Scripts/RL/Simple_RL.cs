using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_RL : MonoBehaviour
{

    // Use this for initialization
    Player player;
    GameManager gameManager;
    float[,] R;
    float[,] Q;
    Vector2 currentPosition;
    Vector2 gridSize;

    public void getNextMoveDirection()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (player.moveUp())
            {
                currentPosition += new Vector2(0, -1);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (player.moveDown())
            {
                currentPosition += new Vector2(0, 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (player.moveLeft())
            {
                currentPosition += new Vector2(-1, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (player.moveRight())
            {
                currentPosition += new Vector2(1, 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            player.resetPosition();
        }
    }

    void Start () {
        player = transform.parent.gameObject.GetComponent<Player>();
        this.gameManager = GameManager.gameManager;
         
        gridSize = this.gameManager.getGridSize();
        this.R = new float[(int)gridSize.x * (int)gridSize.y, 4];
        currentPosition = new Vector2(0, 0);
        fillInitialFactors();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.isMoveFinished())
        {
            FileReader file = new FileReader();
            file.saveToFile(R);
        }
        discoverMaze();

        getNextMoveDirection();
        
    }

    void fillInitialFactors()
    {
        for(int i =0; i < R.GetLength(0); i++)
        {
            for(int j =0; j< R.GetLength(1); j++)
            {
                R[i, j] = 0;
            }
        }
    }

    void discoverMaze()
    {
        PlayerSurroundings player = this.player.getSurroundings();
        foreach(Direction direction in player.walls)
        {
            switch (direction)
            {
                case Direction.UP: placeWallInFactors(new Vector2(0, -1), Direction.UP); break;
                case Direction.RIGHT: placeWallInFactors(new Vector2(1, 0), Direction.RIGHT); break;
                case Direction.DOWN: placeWallInFactors(new Vector2(0, 1), Direction.DOWN); break;
                case Direction.LEFT: placeWallInFactors(new Vector2(-1, 0), Direction.LEFT); break;
            }
        }


            switch (player.finish)
            {
                case Direction.UP: placeFinishInRewards(new Vector2(0, -1), Direction.UP); break;
                case Direction.RIGHT: placeFinishInRewards(new Vector2(1, 0), Direction.RIGHT); break;
                case Direction.DOWN: placeFinishInRewards(new Vector2(0, 1), Direction.DOWN); break;
                case Direction.LEFT:placeFinishInRewards(new Vector2(-1, 0), Direction.LEFT); break;
            }

    }

    void placeWallInFactors(Vector2 positionDiff, Direction direction)
    {
        Vector2 wallPosition = currentPosition + positionDiff;

        if (isValidIndex(wallPosition))
        {
            R[(int)currentPosition.y * (int)gridSize.x + (int)currentPosition.x, (int)direction] = -1;
        }
    }

    void placeFinishInRewards(Vector2 positionDiff, Direction direction)
    {
        Vector2 wallPosition = currentPosition + positionDiff;

        if (isValidIndex(wallPosition))
        {
            R[(int)currentPosition.y*(int)gridSize.x + (int)currentPosition.x, (int)direction] = 100;
        }
    }
    
    bool isValidIndex(Vector2 wallPosition)
    {
        return wallPosition.x >= 0 && wallPosition.x < gridSize.x && wallPosition.y >= 0 && wallPosition.y < gridSize.y;
    }
}
