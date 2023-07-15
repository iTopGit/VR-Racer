using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isPlaying { set; get; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TogglePlaying()
    {
        if (isPlaying)
            isPlaying = false;
        else
            isPlaying = true;
        Debug.Log(isPlaying);
    }

    public void ToMainScene()
    {
        if(!isPlaying)
            SceneManager.LoadSceneAsync("Main scene");
    }

    public void ToTestScene()
    {
        if (!isPlaying)
            SceneManager.LoadSceneAsync("Test scene");
    }
}
