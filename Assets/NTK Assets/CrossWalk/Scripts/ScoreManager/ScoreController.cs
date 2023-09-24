using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    int score;

    bool waitHuman;
    bool crash;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        waitHuman = false;
        crash = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateScore(int score)
    {
        this.score += score;
    }

    public void playerPassed()
    {
        if (!waitHuman) 
        {
            Debug.Log("Not Stop : " + "Score " + score + " to " + (score-50));
            updateScore(-50);
        } 
    }

    void humanPassed()
    {

        waitHuman = true;
        if (crash)
        {
            Debug.Log("Car Crash : " + "Score " + score + " to " + (score - 100));
            updateScore(-100);
        } else
        {
            Debug.Log("Sucess : " + "Score " + score + " to " + (score + 100));
            updateScore(100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            humanPassed();
        }
    }


}
