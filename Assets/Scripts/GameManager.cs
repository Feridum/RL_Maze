using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Vector2 gridSize;

    [SerializeField]
    private Vector2 gridOffset;

    public static GameManager gameManager = null;
    Vector2 tileDiff;
    Vector2 translations;
    private List<int> emptyPlaces;
    public Vector2 playerStartPosition = new Vector2(0, 0);

    public int emptyPlacesNumber {
        get
        {
            return emptyPlaces.Count;
        }
    }

    void Awake()
    {
       
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void initializeParametrs()
    {
        setTileDiff();
        emptyPlaces = Enumerable.Range(1, (int)gridSize.x * (int)gridSize.y - 1).ToList();
    }
    public void setTranslations(Vector2 translations)
    {
        this.translations = translations;
    }

    public void addObstacleToMaze(Vector2 position)
    {
        emptyPlaces.Remove(calulacteFieldNumber(position));
    }

    public void removeObstacleFromMaze(Vector2 position)
    {
        emptyPlaces.Add(calulacteFieldNumber(position));
        emptyPlaces.Sort();
    }

    int calulacteFieldNumber(Vector2 position)
    {
        int x = (int)(position.x - translations.x);
        int y = Mathf.Abs((int)(position.y + translations.y));

        return x + y * (int)gridSize.x;
    }
    public void setTileDiff()
    {
        float x0 = getXPosition(0);
        float x1 = getXPosition(1);

        float y0 = getYPosition((int)gridSize.y - 1);
        float y1 = getYPosition((int)gridSize.y - 2);

        this.tileDiff = new Vector2(x1 - x0, y0 - y1);
    }

    public Vector2 getTileDiff()
    {
        return this.tileDiff;
    }

    public Vector2 getGridSize()
    {
        return this.gridSize;
    }

    public Vector2 getGridOffset()
    {
        return this.gridOffset;
    }

    public float getXPosition(int x)
    {
        return x + gridOffset.x * x + this.translations.x;
    }

    public float getYPosition(int y)
    {
        return -(y + gridOffset.y * y) - this.translations.y;
    }

    public Vector2 getRandomPosition(bool removeField) {
        System.Random random = new System.Random();
        int number = emptyPlaces[random.Next(0, emptyPlaces.Count)];
        if (removeField)
        {
            emptyPlaces.Remove(number);
        }

        return getCoordinatesFromPositionNumber(number);
    }

    Vector2 getCoordinatesFromPositionNumber(int number)
    {
        int y = number / (int)gridSize.x;
        int x = number % (int)gridSize.x;

        playerStartPosition = new Vector2(x, y);

        return new Vector2(getXPosition(x), getYPosition(y));

    }
}
