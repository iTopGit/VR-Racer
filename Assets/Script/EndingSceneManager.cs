using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class EndingSceneManager : MonoBehaviour {
    private bool allowToBack = false;
    public GameObject score;
    public GameObject old_score;
    public GameObject avg_speed;
    public GameObject old_avg_speed;
    public GameObject loadingText;
    public GameObject toMenuButton;

    // public GameObject loadingText;

    public Button resendButton;

    SendCSV sendCSV;
    SendVID sendVID;
    // Start is called before the first frame update
    void Start() {
        sendCSV = gameObject.GetComponent<SendCSV>();
        sendVID = gameObject.GetComponent<SendVID>();

        if (UserContainer.email == null) { UserContainer.email = "vokox44702@utwoko.com"; }

        resendButton.onClick.AddListener(resendFile);
        StartCoroutine(initiating());
    }

    // Update is called once per frame
    void Update() { 
        if(UserContainer.score != null) { 
            score.GetComponent<TextMeshProUGUI>().text = UserContainer.score.ToString(); 
            old_score.GetComponent<TextMeshProUGUI>().text = UserContainer.score_compare.ToString(); 
            avg_speed.GetComponent<TextMeshProUGUI>().text = UserContainer.speed.ToString(); 
            old_avg_speed.GetComponent<TextMeshProUGUI>().text = UserContainer.speed_compare.ToString(); 
        } 
    }

    IEnumerator initiating()
    {
        yield return new WaitForSeconds(2.5f);
        sendFile();
    }
    void resendFile() { 
        sendFile();
        resendButton.gameObject.SetActive(false);
    }
    void sendFile() { 
        sendCSV.SendCSVToServer(UserContainer.video1, UserContainer.video2); 
    }

    // Button within, Press Button to Back to Main Menu.
    public void backToTitle() { SceneManager.LoadScene(1);  }

    public void isLoaded(bool isComplete) {
        if (isComplete) {
            loadingText.GetComponent<TextMeshProUGUI>().text = "Upload video complete.";
            toMenuButton.SetActive(true);
            loadingText.SetActive(false);
        } else {
            loadingText.GetComponent<TextMeshProUGUI>().text = "Uploading File Failed,\nTry resend them again.";
            resendButton.gameObject.SetActive(true);
        }
    }

    public async void sendVDO() { 
        loadingText.GetComponent<TextMeshProUGUI>().text = "loading Video."; 
        await sendVID.uploadVideo(UserContainer.video1, UserContainer.video2); }
}
