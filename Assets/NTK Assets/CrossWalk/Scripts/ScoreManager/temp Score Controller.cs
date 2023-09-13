using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScoreController : MonoBehaviour
{
    int score = 0;
    bool crosswalk = false;
    bool crash = false;
    bool human = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScore(int score)
    {
        this.score += score;
        // Debug.Log(score);
    }

    public void crossWalkPassed()
    {
        crosswalk = true;
        if (!human) {
            updateScore(-50);
        }
    }



    void humanPassed() 
    { 
        human = true;
        if (crash) { 
            updateScore(-100);
        }
        if (!crosswalk && !crash) { 
            updateScore(100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC")) {
            crash = other.GetComponent<HumanController>().dead;
            humanPassed();
        }
    }


}
