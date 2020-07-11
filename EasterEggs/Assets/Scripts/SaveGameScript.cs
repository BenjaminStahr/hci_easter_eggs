using System;
using System.IO;
using UnityEngine;

public class SaveGameScript : MonoBehaviour
{
    //This class stores information for each of the levels.
    public void SaveGame(string Content)
    {
        string fileName = Application.persistentDataPath + "/settings.txt";
        if (!File.Exists(fileName))
        {
            string[] Scores = new string[1];
            Scores[0] = Content;
            File.WriteAllLines(fileName, Scores);
        }
        else
        {
            File.AppendAllText(fileName,
                   Content + Environment.NewLine);
        }
    }
}