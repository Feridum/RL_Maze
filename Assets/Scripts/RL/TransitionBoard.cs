using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoard : MonoBehaviour {

    [SerializeField]
    private GameObject arrow;

    RLManager rlManager;
    GameManager gameManager;
    Translation translation;
    GameObject[] placedArrows;
    void Start () {
        rlManager = RLManager.rlManager;
        gameManager = GameManager.gameManager;
        translation = gameManager.getTranslationForGrid("transition");
        Vector2 gridSize = gameManager.getGridSize();
        placedArrows = new GameObject[(int)(gridSize.x * gridSize.y)];
	}
	
	// Update is called once per frame
	void Update () {
        fillTrasitionBoard();
	}


    void fillTrasitionBoard()
    {
        float[,] Q= rlManager.getQ();
        float[,] R = rlManager.getR();

        if (Q != null)
        {
            for (int i = 0; i < Q.GetLength(0); i++)
            {
                float max = -100;
                int maxIndex = -100;
                bool isOnlyZero = true;
                for(int j = 0; j<Q.GetLength(1); j++)
                {
                    if (R[i, j] != -100)
                    {
                        if (Q[i, j] != 0)
                        {
                            isOnlyZero = false;
                        }
                        if (Q[i, j] >= max)
                        {
                            max = Q[i, j];
                            maxIndex = j;
                        }
                    }
                }

                if (max != 0 || !isOnlyZero)
                    placeArrow(i,maxIndex);
            }
        }

    }

    void placeArrow(int position, int direction)
    {
        Destroy(placedArrows[position]);
        Vector3 arrowPosition = translation.getCoordinatesFromPositionNumber(position);
        arrowPosition.z = -1;
        placedArrows[position] = Instantiate(arrow, arrowPosition, Quaternion.Euler(0, 0, getRotationForDirection(direction)));
        placedArrows[position].transform.parent = gameObject.transform;
    }

    int getRotationForDirection(int direction)
    {
        switch (direction)
        {
            case 0: return 90;
            case 1: return 0;
            case 2: return -90;
            case 3: return 180;
        }
        return 0;
    }
}
