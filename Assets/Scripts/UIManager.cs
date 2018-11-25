using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    InputField xSize;

    [SerializeField]
    InputField ySize;

    [SerializeField]
    InputField xOffset;

    [SerializeField]
    InputField yOffset;

    GridManager gridManager;
    public void Start()
    {
        gridManager = GridManager.gridManager;
        //Adds a listener to the main input field and invokes a method when the value changes.

        Vector2 gridSize = gridManager.getGridSize();
        Vector2 gridOffset = gridManager.getGridOffset();

        xSize.text = gridSize.x.ToString();
        ySize.text = gridSize.y.ToString();

        xOffset.text = gridOffset.x.ToString();
        yOffset.text = gridOffset.y.ToString();

        xSize.onValueChanged.AddListener(delegate { setGridSize(); });
        ySize.onValueChanged.AddListener(delegate { setGridSize(); });

        xOffset.onValueChanged.AddListener(delegate { setGridOffset(); });
        yOffset.onValueChanged.AddListener(delegate { setGridOffset(); });
    }

    // Invoked when the value of the text field changes.
    public void setGridSize()
    {
        float xSizeValue = float.Parse(xSize.text);
        float ySizeValue = float.Parse(ySize.text);

        gridManager.setGridSize(new Vector2(xSizeValue, ySizeValue));
    }

    public void setGridOffset()
    {
        float xSizeValue = float.Parse(xOffset.text);
        float ySizeValue = float.Parse(yOffset.text);

        if(xSizeValue < 0)
        {
            xSizeValue = -xSizeValue;
        }

        if (ySizeValue < 0)
        {
            ySizeValue = -ySizeValue;
        }

        gridManager.setGridOffset(new Vector2(xSizeValue, ySizeValue));
    }
}
