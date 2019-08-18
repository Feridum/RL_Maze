using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridObjects : MonoBehaviour {

    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameObject finishObject;

    [SerializeField]
    private GameObject wallObject;


    GameManager gameManager;
    GridManager gridManager;
    Translation translation;

    void Start () {
        this.gameManager = GameManager.gameManager;
        this.gridManager = GridManager.gridManager;
        translation = gameManager.getTranslationForGrid("main");

        this.createMaze();
    }

    private void placePlayer(int x, int y)
    {
        Vector3 position = new Vector3(translation.getXPosition(x), translation.getYPosition(y), -1);
        gameManager.setPlayerPosition(new Vector2(x,y));
        Instantiate(playerObject, position, Quaternion.identity);
    }

    private void createFinish(int x, int y)
    {
        Vector3 position = new Vector3(translation.getXPosition(x), translation.getYPosition(y), -1);
        Instantiate(finishObject, position, Quaternion.identity);
    }


    private void createMaze()
    {
        string[] lines = gridManager.getMazeInformation();
        int column = 0;
        for (int row =0; row < 10; row++)
        {
            column = 0;
            foreach(char c in lines[row])
            {
                if(c == GridManager.emptyPlace) { }
                else if(c == GridManager.wall)
                {
                    Vector3 position = new Vector3(translation.getXPosition(column), translation.getYPosition(row), -1);
                    gameManager.addObstacleToMaze(position);
                    Instantiate(wallObject, position, Quaternion.identity);
                }else if(c == GridManager.start)
                {
                    placePlayer(column, row);
                }
                else if(c == GridManager.end)
                {
                    createFinish(column, row);
                }

                column = column + 1;
            }
        }
    }

}
