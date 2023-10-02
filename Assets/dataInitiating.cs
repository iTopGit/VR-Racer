using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class dataInitiating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script Initiated");
        DeleteFiles();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DeleteFiles()
    {
        // Get the path to the streamingAssetsPath directory.
        string streamingAssetsPath = Application.streamingAssetsPath;
        
        // Function called.
        Debug.Log(streamingAssetsPath);

        // Get all files in the streamingAssetsPath directory.
        FileInfo[] files = Directory.GetFiles(streamingAssetsPath).Select(x => new FileInfo(x)).ToArray();

        // Loop through all files and delete any .csv and .mp4 files.
        foreach (FileInfo file in files)
        {
            if (file.Extension == ".csv" || file.Extension == ".mp4")
            {
                File.Delete(file.FullName);
            }
        }
        Debug.Log("Finish Deleting.");
    }
}
