using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private Vector2 gridSize = new Vector2(5,5);

    [SerializeField]
    private Vector2 gridOffset = new Vector2((float)0.05, (float)0.05);

    public static GridManager gridManager = null;
    void Awake()
    {

        if (gridManager == null)
        {
            gridManager = this;

        }
        else if (gridManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setGridSize(Vector2 size)
    {
        this.gridSize = size;
    }

    public Vector2 getGridSize()
    {
        return this.gridSize;
    }

    public Vector2 getGridOffset()
    {
        return this.gridOffset;
    }

    public void setGridOffset(Vector2 offset)
    {
        this.gridOffset = offset;
    }
}
