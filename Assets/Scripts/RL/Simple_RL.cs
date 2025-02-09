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
    RLManager rlManager;
    DataCollectionManager dataCollectionManager;
    float[,] R;
    float[,] Q;
    Vector2 currentPosition;
    Vector2 gridSize;
    float futureStepsRate = 0.6f;
    float learningRate = 1.0f;
    bool learnStart = false;
    bool moveStart = false;
    bool fileSaved = false;
    Vector2 initialPosition;


    int stepsToFinish = 0;
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        gameManager = GameManager.gameManager;
        rlManager = RLManager.rlManager;
        dataCollectionManager = DataCollectionManager.dataCollectionManager;
        initialPosition = gameManager.playerStartPosition;
        gridSize = this.gameManager.getGridSize();

        startNewEpoche();
    }

    void startNewEpoche()
    {
        this.R = new float[(int)gridSize.x * (int)gridSize.y, 4];
        this.Q = new float[(int)gridSize.x * (int)gridSize.y, 4];
        currentPosition = initialPosition;
        fillInitialFactors();
    }

    void Update()
    {
        rlManager.updateQ(Q);
        rlManager.updateR(R);

        if (player.isMoveFinished())
        {
            rlManager.saveIterationInformation(stepsToFinish);
            stepsToFinish = 0;
            gameManager.setPlayerPosition(currentPosition);
            moveStart = false;
            if(rlManager.shouldStartNewIteration())
            {
                gameManager.setPlayerPosition(initialPosition);
                placePlayer(false);
            }
            else if (rlManager.shouldStartNewEpoche())
            {
                gameManager.setPlayerPosition(initialPosition);
                startNewEpoche();
                placePlayer(false);
            }
            else
            {
                learnStart = false;
                if (!fileSaved)
                {
                    FileReader file = new FileReader();
                    file.saveToFile(R, "r.txt");
                    file.saveToFile(Q, "q.txt");
                    rlManager.saveResults();
                    fileSaved = true;
                }
                player.startMove();
            }
        }


        if (moveStart)
        { 
            discoverMaze();
            getNextMoveDirection();
            stepsToFinish++;
            fileSaved = false;
        }

        if (!learnStart && Input.GetKeyUp(KeyCode.L))
        {
            learnStart = true;
            placePlayer();
        }
        if (!learnStart && Input.GetKeyUp(KeyCode.S))
        {
            placePlayer();
        }
        if (!learnStart && Input.GetKeyUp(KeyCode.R))
        {
            placePlayer(true);
        }
    }

    void placePlayer(bool placeOnRandom = false)
    {
        if (placeOnRandom)
        {
            player.placeOnRandomPosition(true);
        }
        else
        {
            player.placeOnStartingPosition();
        }
        currentPosition = gameManager.playerStartPosition;
        moveStart = true;
        player.startMove();
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
        clearRewards();
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


    void clearRewards ()
    {
        R[getPositionInArray(currentPosition), (int)Direction.UP] = 0;
        R[getPositionInArray(currentPosition), (int)Direction.LEFT] = 0;
        R[getPositionInArray(currentPosition), (int)Direction.DOWN] = 0;
        R[getPositionInArray(currentPosition), (int)Direction.RIGHT] = 0;
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
            float reward = R[getPositionInArray(currentPosition), i];
            float positionInQ = Q[getPositionInArray(currentPosition), i];
            if (reward == -100)
            {
                QRow[i] = -1000;
            }else if(reward == 100 && positionInQ == 0)
            {
                QRow[i] = 100;
            }
            else
            {
                QRow[i] = positionInQ;
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
        Q[getPositionInArray(previousPosition), directionValue] = (1-learningRate)* Q[getPositionInArray(previousPosition), directionValue] + learningRate *(reward + futureStepsRate * maxNextQValue)-(float)0.5;
    }
}
