using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private InterfaceManager IM;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        IM = GameObject.Find("Interface Manager").GetComponent<InterfaceManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(selectButton);
    }

    // Update is called once per frame
    void Update() { }

    void selectButton()
    {
        string buttonName = button.gameObject.name;
        Debug.Log(buttonName + " was clicked");

        if (buttonName == "Start Button")
        {
            IM.setStart();
        } 
        else if (buttonName == "Tutorial Button") 
        {
            IM.setTutorial();
        } 
        else if (buttonName == "Back Button")
        {
            IM.setTitle();
        }
        else if (buttonName == "Exit Button")
        {
            Application.Quit();
        }

        
    }
}
