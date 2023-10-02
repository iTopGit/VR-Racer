using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class InterfaceManager : MonoBehaviour
{
    public GameObject titleScreen, tutorialScreen, scoreScreen;
    public Button startButton, backButton, startAgainButton;

    string username;
    public TextMeshProUGUI usernameText;

    // Start is called before the first frame update
    void Start() {
        // Check if XR (VR/AR) is supported
        if (XRSettings.isDeviceActive)
        {
            // Disable head tracking
            XRSettings.enabled = false;
        }

        //LoginManager LM = GameObject.Find("Login Manager").GetComponent<LoginManager>();
        username = UserContainer.username;
        Debug.Log(username);
        usernameText.text = "Username : " + username;

    }
    // Update is called once per frame
    void Update() { 

    }

    public void setStart() 
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void setTutorial()
    {
        // Swap Panel
        titleScreen.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
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
    public void setScore()
    {
        // Swap Panel
        tutorialScreen.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(true);
        // Start Button Selected
        EventSystem.current.SetSelectedGameObject(startAgainButton.gameObject);
    }
}
