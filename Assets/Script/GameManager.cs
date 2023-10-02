using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public InputActionProperty showButton;
    public GameObject menuPanel;

    public GameObject titleScreen;
    public GameObject tutorialScreen;
    public GameObject scoreScreen;


    public bool isPlaying;

    private int score;

    public Button titleButton;
    public Button toTutorialButton;
    // public Button toTitleButton;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Scene 2: " + UserContainer.email);
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menuPanel.SetActive(true);
        }

        if(Input.GetKey("l"))
        {
            SceneManager.LoadScene(2);
        }
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

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        //scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        //gameOverText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
        //isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        titleScreen.gameObject.SetActive(false);

        isPlaying = true;
        score = 0;
        UpdateScore(0);
        SceneManager.LoadScene(2);
    }

    // Button within Main Menu, Press Button to go to tutorial screen.
    public void tutorial() 
    {
        titleScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(toTutorialButton.gameObject);
    }

    // Button within, Press Button to Back to Main Menu.
    public void backToTitle()
    {
        tutorialScreen.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(titleButton.gameObject);
    }

    // Button within Main Menu.
    public void exitGame() {
        Application.Quit();
    }
}
