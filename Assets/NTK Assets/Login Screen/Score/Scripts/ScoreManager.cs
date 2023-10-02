using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI displayScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateScore(int scoreToAdd) 
    { 
        score += scoreToAdd;
        displayScore.text = score.ToString();
    }
} 
