using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingSceneManager : MonoBehaviour {
    public InputActionProperty showButton;

    public Button titleButton;
    GameObject menuPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menuPanel.SetActive(true);
        }
    }

    // Button within, Press Button to Back to Main Menu.
    public void backToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
