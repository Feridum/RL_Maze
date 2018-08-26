using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class GridCreate : MonoBehaviour {

        [SerializeField]
        private GameObject floorObject;

        [SerializeField]
        private GameObject playerObject;

        [SerializeField]
        private GameObject finishObject;


        private Vector2 gridSize;
        private Vector2 gridOffset;
        GameManager gameManager;
        float xTranslation;
        float yTranslation; 
        void Start() {
            this.gameManager = GameManager.gameManager;
            gridSize = gameManager.getGridSize();
            gridOffset = gameManager.getGridOffset();

            calculateFloorDimesions();
            this.xTranslation = transform.position.x - gridSize.x / 2 + floorObject.GetComponent<Renderer>().bounds.size.x / 2;
            this.yTranslation = transform.position.y - gridSize.y / 2 - floorObject.GetComponent<Renderer>().bounds.size.y / 2;
            this.setBoardConstants();

            createBoard();
            this.createPlayer();
            this.createFinish();
        }

        void setBoardConstants()
        {
            this.gameManager.setTranslations(new Vector2(xTranslation, yTranslation));
            gameManager.initializeParametrs();
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
            float xPos = gameManager.getXPosition(x);
            float yPos = gameManager.getYPosition(y);

            Instantiate(floorObject, new Vector2(xPos, yPos), Quaternion.identity);
        }

       

        private void calculateFloorDimesions()
        {

            Sprite sprite = floorObject.GetComponent<SpriteRenderer>().sprite;
            
            float scaleX = 1 / (sprite.bounds.size.x + gridOffset.x);
            float scaleY = 1 / (sprite.bounds.size.y + gridOffset.y);
            floorObject.transform.localScale = new Vector2(scaleX, scaleY);
        }

        private void OnDrawGizmos()
        {
            GameManager daBoss = (GameManager)FindObjectOfType(typeof(GameManager));
            Gizmos.DrawWireCube(transform.position, daBoss.getGridSize());
        }

        private void createPlayer()
        {
            Vector3 position = new Vector3(gameManager.getXPosition(0), gameManager.getYPosition(0), -1);
            Instantiate(playerObject,position, Quaternion.identity);
        }

        private void createFinish()
        {
            Vector3 position = new Vector3(gameManager.getXPosition((int)gridSize.x-1), gameManager.getYPosition((int)gridSize.y - 1), -1);
            Instantiate(finishObject, position, Quaternion.identity);
        }
    }
}