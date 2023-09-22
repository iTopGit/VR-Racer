using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public GameObject titleScreen, tutorialScreen;
    public Button startButton, backButton;

    public bool signin = true;
    public bool interfaceActivated = false;
    // Start is called before the first frame update
    void Start() {
        if (!signin)
        {
            titleScreen.gameObject.SetActive(false);
            Application.OpenURL("https://cmu.to/VRacer");
        }
    }
    // Update is called once per frame
    void Update() { 
        if (signin && !interfaceActivated)
        {
            titleScreen.gameObject.SetActive(true);
            interfaceActivated = true;
        }
    }

    public void setStart() 
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void setTutorial()
    {
        // Swap Panel
        titleScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
        // Back Button Selected
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }
    public void setTitle()
    {
        // Swap Panel
        titleScreen.gameObject.SetActive(true);
        tutorialScreen.gameObject.SetActive(false);
        // Start Button Selected
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }
}
