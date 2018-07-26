using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager = null;
    Vector2 startingTile;
    Vector2 tileDiff;

    void Awake()
    {
       
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    public void setStartingTile(Vector2 startingTile)
    {
        this.startingTile = startingTile;
    }

    public void setTileDiff(Vector2 tileDiff)
    {
        this.tileDiff = tileDiff;
    }

    public Vector2 getStartingTile()
    {
       return this.startingTile;
    }

    public Vector2 getTileDiff()
    {
        return this.tileDiff;
    }
}
