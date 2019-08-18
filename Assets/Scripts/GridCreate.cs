using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class GridCreate : MonoBehaviour
    {

        [SerializeField]
        private GameObject tileObject;

        [SerializeField]
        private string boardName = "main";

        [SerializeField]
        private bool stickToRight = true;

        Vector2 gridSize;
        Vector2 gridOffset;
        GameManager gameManager;
        float xTranslation;
        float yTranslation;

        Translation translation;
        string[] fileLines;

        void Start()
        {
            this.gameManager = GameManager.gameManager;
            gridSize = gameManager.getGridSize();
            gridOffset = gameManager.getGridOffset();

            calculateFloorDimesions();
            transform.position = getInitialPosition();
            translation = new Translation(transform.position, gridSize, gridOffset, tileObject.GetComponent<Renderer>().bounds.size);
            this.setBoardConstants();


            createBoard();
        }

        void setBoardConstants()
        {
            gameManager.initializeParametrs();
            this.gameManager.setTranslation(boardName, translation);
        }

        void createBoard()
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    this.placeBlockOnPosition(x, y);
                }
            }

        }

        void placeBlockOnPosition(int x, int y)
        {
            float xPos = translation.getXPosition(x);
            float yPos = translation.getYPosition(y);

            GameObject block = Instantiate(tileObject, new Vector2(xPos, yPos), Quaternion.identity);
            block.transform.parent = gameObject.transform;
        }

        private void calculateFloorDimesions()
        {

            Sprite sprite = tileObject.GetComponent<SpriteRenderer>().sprite;

            float scaleX = 1 / (sprite.bounds.size.x + gridOffset.x);
            float scaleY = 1 / (sprite.bounds.size.y + gridOffset.y);
            tileObject.transform.localScale = new Vector2(scaleX, scaleY);
        }

        private void OnDrawGizmos()
        {
            GridManager gridManager = (GridManager)FindObjectOfType(typeof(GridManager));
            Vector3 gridSize = gridManager.getGridSize();
            Gizmos.DrawWireCube(getInitialPosition(), gridSize);
        }

        private Vector3 getInitialPosition()
        {
            Camera cam = Camera.main;
            int size = (int)(cam.orthographicSize / 2);
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            float xPosition = 0;


            if (stickToRight)
            {
                xPosition = width / 4;
            }
            else
            {
                xPosition = -width / 4;
            }

            Vector3 initialPosition = new Vector3();
            initialPosition = new Vector3(0,0,0) + new Vector3(xPosition, 0, 0);

            return initialPosition;
        }

    }
}