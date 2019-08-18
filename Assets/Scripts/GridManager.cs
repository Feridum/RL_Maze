using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private Vector2 gridOffset = new Vector2((float)0.05, (float)0.05);

    [SerializeField]
    private TextAsset mazeFile;

    [SerializeField]
    public static char emptyPlace = '.';

    [SerializeField]
    public static char wall = '#';

    [SerializeField]
    public static char start = 'B';

    [SerializeField]
    public static char end = 'E';

    private Vector2 gridSize;
    string[] fileLines;
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

    void Start()
    {
        fileLines = mazeFile.text.Split('\n');
        this.gridSize = new Vector2(float.Parse(fileLines[0]), float.Parse(fileLines[0]));
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

    public string[] getMazeInformation()
    {
        return fileLines.Skip(1).ToArray();
    }
}
