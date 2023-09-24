using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    private Button button;
    private GameManager gameManager;

    private int select = -1;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(selectButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            gameManager.showScore();
        }
    }

    void selectButton()
    {
        string buttonName = button.gameObject.name;
        Debug.Log(buttonName + " was clicked");

        if (buttonName == "Start Button")
        {
            gameManager.startGame();
        } else if (buttonName == "Tutorial Button") 
        {
            gameManager.tutorial();
            //EventSystem.current.SetSelectedGameObject(backButton.gameObject);
        } else if (buttonName == "Back Button")
        {
            gameManager.backToTitle();
        }
        else if (buttonName == "Exit Button")
        {
            gameManager.exitGame();
        }

        
    }
}
