using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class GridCreate : MonoBehaviour {

        [SerializeField]
        private GameObject tileObject;

        [SerializeField]
        private string boardName = "main";

        Vector2 gridSize;
        Vector2 gridOffset;
        GameManager gameManager;
        float xTranslation;
        float yTranslation;

        Translation translation;

        void Start() {
            this.gameManager = GameManager.gameManager;
            gridSize = gameManager.getGridSize();
            gridOffset = gameManager.getGridOffset();

            calculateFloorDimesions();

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
            for(int y = 0; y < gridSize.y; y++)
            {
                for(int x = 0; x<gridSize.x; x++)
                {
                    this.placeBlockOnPosition(x, y); 
                }
            }
           
        }
        
        void placeBlockOnPosition(int x, int y)
        {
            float xPos = translation.getXPosition(x);
            float yPos = translation.getYPosition(y);

            Instantiate(tileObject, new Vector2(xPos, yPos), Quaternion.identity);
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
            GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
            Gizmos.DrawWireCube(transform.position, gameManager.getGridSize());
        }

       
    }
}