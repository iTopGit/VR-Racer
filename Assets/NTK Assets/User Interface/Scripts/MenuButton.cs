using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    private Button button;
    GameManager gameManager;
    EndingSceneManager backManager;

    private int select = -1;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.FindGameObjectWithTag("sceneManager").GetComponent<GameManager>();
        backManager = GameObject.FindGameObjectWithTag("sceneManager").GetComponent<EndingSceneManager>();
        button.onClick.AddListener(selectButton);
    }

    void selectButton()
    {
        if(gameManager != null || backManager != null) {
            string buttonName = button.gameObject.name;
            // Debug.Log(buttonName + " was clicked");

            if (buttonName == "Start Button") {
                gameManager.startGame();
            } else if (buttonName == "Tutorial Button") {
                gameManager.tutorial();
            } else if (buttonName == "Back Button") {
                if(gameManager != null) {
                    gameManager.backToTitle();
                } else if (backManager != null) {
                    backManager.backToTitle();
                }
            } else if (buttonName == "Exit Button") {
                gameManager.exitGame();
            }
        }
    }
}
