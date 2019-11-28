using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataRow
{
    public int epoche;
    public int iteration;
    public int steps;

    public DataRow(int epoche, int iteration, int steps)
    {
        this.epoche = epoche;
        this.iteration = iteration;
        this.steps = steps;
    }

    public string getCSVString()
    {
        return epoche.ToString() + ';' + iteration.ToString() + ';' + steps.ToString();
    }
  
}

public class DataCollectionManager
{
    public static DataCollectionManager dataCollectionManager;
    GameManager gameManager;
    private List<DataRow> dataRowList = new List<DataRow>();
    FileReader fileReader;
    // Start is called before the first frame update


    public DataCollectionManager()
    {
        gameManager = GameManager.gameManager;
        fileReader = new FileReader();
    }


    public void addNewData(DataRow dataRow)
    {
        dataRowList.Add(dataRow);
    }

    public void saveResults()
    {
        StreamWriter file = fileReader.startFileWriting("result.txt");
        dataRowList.ForEach(delegate (DataRow dataRow)
        {
            fileReader.saveLine(file, dataRow.getCSVString());
        });

        file.Close();
    }

    public List<DataRow> getDataRows()
    {
        return dataRowList;
    }
}
