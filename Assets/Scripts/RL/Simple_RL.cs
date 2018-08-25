﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Simple_RL : MonoBehaviour
{

    // Use this for initialization
    Player player;
    GameManager gameManager;
    float[,] R;
    float[,] Q;
    Vector2 currentPosition;
    Vector2 gridSize;
    float learningRate = 0.6f;
    bool learnStart = false;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        this.gameManager = GameManager.gameManager;

        gridSize = this.gameManager.getGridSize();
        this.R = new float[(int)gridSize.x * (int)gridSize.y, 4];
        this.Q = new float[(int)gridSize.x * (int)gridSize.y, 4];
        currentPosition = new Vector2(0, 0);
        fillInitialFactors();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isMoveFinished())
        {
            FileReader file = new FileReader();
            file.saveToFile(R, "r.txt");
            file.saveToFile(Q, "q.txt");
            learnStart = false;
        }


        if (learnStart)
        {
            discoverMaze();

            getNextMoveDirection();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            learnStart = true;
        }
    }

    void fillInitialFactors()
    {
        for (int i = 0; i < R.GetLength(0); i++)
        {
            for (int j = 0; j < R.GetLength(1); j++)
            {
                R[i, j] = 0;
            }
        }
    }

    void discoverMaze()
    {
        PlayerSurroundings player = this.player.getSurroundings();
        foreach (Direction direction in player.walls)
        {
            switch (direction)
            {
                case Direction.UP: placeWallInFactors(Direction.UP); break;
                case Direction.RIGHT: placeWallInFactors(Direction.RIGHT); break;
                case Direction.DOWN: placeWallInFactors(Direction.DOWN); break;
                case Direction.LEFT: placeWallInFactors(Direction.LEFT); break;
            }
        }


        switch (player.finish)
        {
            case Direction.UP: placeFinishInRewards(Direction.UP); break;
            case Direction.RIGHT: placeFinishInRewards(Direction.RIGHT); break;
            case Direction.DOWN: placeFinishInRewards(Direction.DOWN); break;
            case Direction.LEFT: placeFinishInRewards(Direction.LEFT); break;
        }

    }

    void placeWallInFactors(Direction direction)
    {
        R[getPositionInArray(currentPosition), (int)direction] = -100;
    }

    void placeFinishInRewards(Direction direction)
    {
        R[getPositionInArray(currentPosition), (int)direction] = 100;
    }

    bool isValidIndex(Vector2 wallPosition)
    {
        return wallPosition.x >= 0 && wallPosition.x < gridSize.x && wallPosition.y >= 0 && wallPosition.y < gridSize.y;
    }

    Vector2 getDiffByDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return new Vector2(0, -1);
            case Direction.RIGHT: return new Vector2(1, 0);
            case Direction.DOWN: return new Vector2(0, 1);
            case Direction.LEFT: return new Vector2(-1, 0);
        }
        return new Vector2(0, 0);
    }

    int getPositionInArray(Vector2 currentPosition)
    {
        return (int)currentPosition.y * (int)gridSize.x + (int)currentPosition.x;
    }
    public void getNextMoveDirection()
    {
        System.Random random = new System.Random();
        float[] QRow = getQRow(currentPosition);
        float maxQ = QRow.Max();
        int[] indexes = QRow.Select((value, index) => new { Value = value, Index = index })
                                  .Where(x => x.Value == maxQ)
                                  .Select(x => x.Index)
                                  .ToArray();

        int indexToMove = indexes[random.Next(0, indexes.Length)];
        

        switch (indexToMove)
        {
            case (int)Direction.UP: makeNextMove(Direction.UP); break;
            case (int)Direction.RIGHT: makeNextMove(Direction.RIGHT); break;
            case (int)Direction.DOWN: makeNextMove(Direction.DOWN); break;
            case (int)Direction.LEFT: makeNextMove(Direction.LEFT); break;
        }

    }


    float[] getQRow(Vector2 currentPosition)
    {
        float[] QRow = new float[4];
        for (int i = 0; i < 4; i++)
        {
            if (R[getPositionInArray(currentPosition), i] == -100)
            {
                QRow[i] = -1000;
            }
            else
            {
                QRow[i] = Q[getPositionInArray(currentPosition), i];
            }
        }

        return QRow;
    }

    void makeNextMove(Direction direction)
    {
        int directionValue = (int)direction;
        Vector2 previousPosition = currentPosition;
        switch (direction)
        {
            case Direction.UP:
                {
                    if (player.moveUp())
                    {
                        currentPosition += getDiffByDirection(Direction.UP);
                    }
                    break;
                }
            case Direction.DOWN:
                {
                    if (player.moveDown())
                    {
                        currentPosition += getDiffByDirection(Direction.DOWN);
                    }
                    break;
                }
            case Direction.LEFT:
                {
                    if (player.moveLeft())
                    {
                        currentPosition += getDiffByDirection(Direction.LEFT);
                    }
                    break;
                }
            case Direction.RIGHT:
                {
                    if (player.moveRight())
                    {
                        currentPosition += getDiffByDirection(Direction.RIGHT);
                    }
                    break;
                }
        }

        float reward = R[getPositionInArray(previousPosition), directionValue];
        float maxNextQValue = getQRow(currentPosition).Max();
        Q[getPositionInArray(previousPosition), directionValue] = reward + learningRate * maxNextQValue;
    }
}
