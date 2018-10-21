﻿using Assets.Scripts;
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
    private List<int> emptyPlaces;
    private List<GridTranslation> gridTranslations = new List<GridTranslation>();
    public Vector2 playerStartPosition = new Vector2(0, 0);
    Translation translation;
    public int emptyPlacesNumber {
        get
        {
            return emptyPlaces.Count;
        }
    }

    void Awake()
    {

        if (gameManager == null)
        {
            gameManager = this;
            
        }
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void initializeParametrs()
    {
        emptyPlaces = Enumerable.Range(1, (int)gridSize.x * (int)gridSize.y - 1).ToList();
    }
    public void setTranslation(string name, Translation translation)
    {
        GridTranslation gridTranslation = new GridTranslation(name, translation);
        gridTranslations.Add(gridTranslation);
        if (name.Equals("main"))
        {
            this.translation = translation;
        }
    }

    public Translation getTranslationForGrid(string name)
    {
        GridTranslation gridTranslation = gridTranslations.Find(x => x.getName().Equals(name));
        return gridTranslation.getTranslation();
    }

    public void addObstacleToMaze(Vector2 position)
    {
        emptyPlaces.Remove(translation.calulacteFieldNumber(position));
    }

    public void removeObstacleFromMaze(Vector2 position)
    {
        emptyPlaces.Add(translation.calulacteFieldNumber(position));
        emptyPlaces.Sort();
    }

    public Vector2 getRandomPosition(bool removeField) {
        System.Random random = new System.Random();
        int number = emptyPlaces[random.Next(0, emptyPlaces.Count)];
        if (removeField)
        {
            emptyPlaces.Remove(number);
            emptyPlaces.Sort();
        }

        return translation.getCoordinatesFromPositionNumber(number);
    }


    public void setPlayerPositionFromVector(Vector2 position)
    {
        setPlayerPosition(translation.getTableIndexesFromPosition(position));
    }

    public void setPlayerPosition(Vector2 position)
    {
        playerStartPosition = position;
    }

    public Vector2 getGridSize()
    {
        return this.gridSize;
    }

    public Vector2 getGridOffset()
    {
        return this.gridOffset;
    }

}
