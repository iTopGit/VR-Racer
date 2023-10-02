using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    public string fileName = "data.csv";
    private string delimiter = ",";

    public void WriteToCSV(List<string[]> rowData)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string[] row in rowData)
            {
                writer.WriteLine(string.Join(delimiter, row));
            }
        }

        Debug.Log("CSV file written to: " + filePath);
    }
}
