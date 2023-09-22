using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject tutorialScreen;
    public GameObject scoreScreen;

    public bool isPlaying;

    private int score;

    public Button titleButton;
    public Button backButton;
    public Button toTitleButton;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Check the name of the currently active scene
        string sceneName = currentScene.name;
        Debug.Log("Currently active scene: " + sceneName);
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
    }

    public void tutorial() 
    {
        titleScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }
    public void backToTitle()
    {
        tutorialScreen.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(titleButton.gameObject);
    }
    public void exitGame() { }

    public void showScore()
    {
        scoreScreen.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(toTitleButton.gameObject);
        isPlaying = false;
    }
}
