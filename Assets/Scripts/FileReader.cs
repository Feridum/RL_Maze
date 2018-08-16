﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class FileReader
    {
        
        public void saveToFile(float[,] array, string path = "test.txt")
        {

            StreamWriter file = new StreamWriter("./Assets/"+path);

            int iLength = array.GetLength(0);
            int jLength = array.GetLength(1);

            for(int i = 0; i < iLength; i++)
            {
                for(int j=0; j< jLength; j++)
                {
                    file.Write("{0}\t", array[i,j]);
                }
                file.WriteLine();
            }
            file.Close();
        }

        public float[,] readFromFile(string path = "test.txt")
        {
            string[] readText = File.ReadAllLines("./Assets/" + path);
            int length = readText.Length;

            float[,] array = new float[length, length];

            for(int i = 0; i< length; i++)
            {
                string[] arrayValues = readText[i].Split('\t'); 

                for(int j = 0; j < length; j++)
                {
                    array[i, j] = float.Parse(arrayValues[j]);
                }
            }

            return array;
        }

    }
}
