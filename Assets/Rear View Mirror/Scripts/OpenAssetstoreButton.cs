using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAssetstoreButton : MonoBehaviour
{
    void Start()
    {
        Application.OpenURL("https://assetstore.unity.com/publishers/7528/");
    }

    public void OpenURL()
    {
        Application.OpenURL("https://assetstore.unity.com/publishers/7528/");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
