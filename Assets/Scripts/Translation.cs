using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Translation
    {
        Vector2 gridSize;
        Vector2 gridOffset;
        Vector2 translation;
        Vector2 tileDiff;
        public Translation(Vector2 gridPosition, Vector2 gridSize, Vector2 gridOffset, Vector2 tileSize)
        {
            translation.x = gridPosition.x - gridSize.x / 2 + tileSize.x / 2;
            translation.y = gridPosition.y + gridSize.y / 2 - tileSize.y / 2;

            this.gridOffset = gridOffset;
            this.gridSize = gridSize;
            calculateTileDiff();
        }
        public float getXPosition(int x)
        {
            return x + gridOffset.x * x + translation.x;
        }

        public float getYPosition(int y)
        {
            return -(y + gridOffset.y * y) + translation.y;
        }

        public int calulacteFieldNumber(Vector2 position)
        {
            int x = (int)(position.x - translation.x);
            int y = Mathf.Abs((int)(position.y - translation.y));

            return x + y * (int)gridSize.x;
        }

        public Vector2 getCoordinatesFromPositionNumber(int number)
        {
            Vector2 tableIndexes = getTableIndexesFromNumber(number);
            return new Vector2(getXPosition((int)tableIndexes.x), getYPosition((int)tableIndexes.y));

        }

        public Vector2 getTableIndexesFromPosition(Vector2 position)
        {
            int x = (int)(position.x - translation.x);
            int y = Mathf.Abs((int)(position.y - translation.y));

            return new Vector2(x,y); ;
        }

        public Vector2 getTableIndexesFromNumber(int number)
        {
            int y = number / (int)gridSize.x;
            int x = number % (int)gridSize.x;

            return new Vector2(x, y);
        }

        void calculateTileDiff()
        {
            float x0 = getXPosition(0);
            float x1 = getXPosition(1);

            float y0 = getYPosition((int)gridSize.y - 1);
            float y1 = getYPosition((int)gridSize.y - 2);

            tileDiff = new Vector2(x1 - x0, y0 - y1);
        }

        public Vector2 getTileDiff()
        {
            return tileDiff;
        }
    }
}
