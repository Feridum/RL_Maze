using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets {
    public class GridCreate : MonoBehaviour {

        [SerializeField]
        private Vector2 gridSize;

        [SerializeField]
        private Vector2 gridOffset;

        [SerializeField]
        private GameObject floorObject;

        [SerializeField]
        private GameObject playerObject;

        [SerializeField]
        private GameObject finishObject;

        GameManager gameManager;
        float xTranslation;
        float yTranslation; 
        void Start() {
            this.gameManager = GameManager.gameManager;
            this.gameManager.setGridSize(this.gridSize);

            calculateFloorDimesions();
            this.xTranslation = transform.position.x - gridSize.x / 2 + floorObject.GetComponent<Renderer>().bounds.size.x / 2;
            this.yTranslation = transform.position.y - gridSize.y / 2 + floorObject.GetComponent<Renderer>().bounds.size.y / 2;
            createBoard();
            this.setBoardConstants();
            this.createPlayer();
            this.createFinish();
        }

        void setBoardConstants()
        {
            float x0 = getXPosition(0);
            float x1 = getXPosition(1);

            float y0 = getYPosition((int)gridSize.y-1);
            float y1 = getYPosition((int)gridSize.y-2);

            this.gameManager.setStartingTile(new Vector2(x0, y0));
            this.gameManager.setTileDiff(new Vector2(x1 - x0, y1 - y0));
        }

        void createBoard()
        {
            for(int y = (int)gridSize.y -1; y >=0; y--)
            {
                for(int x = 0; x<gridSize.x; x++)
                {
                    this.placeBlockOnPosition(x, y); 
                }
            }
           
        }
        
        void placeBlockOnPosition(int x, int y)
        {
            float xPos = getXPosition(x);
            float yPos = getYPosition(y);

            Instantiate(floorObject, new Vector2(xPos, yPos), Quaternion.identity);
        }

        float getXPosition(int x)
        {
            return x + gridOffset.x * x + this.xTranslation;
        }

        float getYPosition(int y)
        {
            return y + gridOffset.y * y + this.yTranslation;
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
            Gizmos.DrawWireCube(transform.position, gridSize);
        }

        private void createPlayer()
        {
            Vector3 position = this.gameManager.getStartingTile();
            position.z = -1;
            Instantiate(playerObject,position, Quaternion.identity);
        }

        private void createFinish()
        {
            Vector3 position = new Vector3(getXPosition((int)gridSize.x-1), getYPosition(0), -1);
            Instantiate(finishObject, position, Quaternion.identity);
        }
    }
}