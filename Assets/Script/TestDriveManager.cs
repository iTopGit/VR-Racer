using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestDriveManager : MonoBehaviour
{

    private int p_score = 0;
    private int s_score = 0;

    public GameObject[] checkpoints;
    public GameObject[] stopAreas;

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textStopScore;

    // Start is called before the first frame update
    void Start()
    {
        ResetTestDrive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTestDrive()
    {
        textScore.text = "Score : 0/"+checkpoints.Length;
        textStopScore.text ="Stop Score : 0/"+stopAreas.Length;
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(true);
        }
        foreach (GameObject stopArea in stopAreas)
        {
            stopArea.GetComponentInChildren<TestDriveStopArea>().ResetStopArea();
        }
    }
    public void ThroughCheckpoint()
    {
        p_score++;
        textScore.text = "Score : "+p_score.ToString()+"/"+checkpoints.Length;
        Debug.Log("CP Score: "+p_score);
    }


    public void CarStops()
    {
        s_score++;
        textStopScore.text = "Stop Score : "+s_score.ToString()+"/"+stopAreas.Length;
        Debug.Log("Stop Score: "+s_score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ResetTestDrive();
        }
    }
}
