using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TestDriveStopArea : MonoBehaviour
{
    private TestDriveManager testDriveManager;
    private GameObject player;
    private GameObject stopArea;

    private float distance = 0;
    private bool isStop = false;
    [SerializeField] private TextMeshProUGUI textDistance;
    [SerializeField] private GameObject image;


    // Start is called before the first frame update
    void Start()
    {
        testDriveManager = GameObject.Find("Test Drive Manager").GetComponent<TestDriveManager>();
        stopArea = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player && !isStop)
        {
            if (player.GetComponent<CarController>().currentSpeed < 1)
            {
                Debug.Log("Stop");
                testDriveManager.CarStops();
                CalDistance();
                isStop = true;
                image.GetComponent<Image>().color = new Color(0f, 0.8f, 0.4f, 0.5f);
                textDistance.text = ((distance/1.5)-1.3).ToString("F2")+ " M";
            }
        }
    }

    public void ResetStopArea()
    {
        stopArea.SetActive(true);
        textDistance.text = "Stop!";
        isStop = false;
        image.GetComponent<Image>().color = new Color(1f, 0.0f, 0.0f, 0.5f);
        distance = 0;
    }

    private void CalDistance()
    {
        // Calculate the horizontal distance between the two objects
        Vector2 position1 = new Vector2(image.transform.position.x, image.transform.position.z);
        Vector2 position2 = new Vector2(player.transform.position.x, player.transform.position.z);
        distance = Vector2.Distance(position1, position2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    public void LeaveArea()
    {
        Debug.Log("Leave");
        stopArea.SetActive(false);
        if (player)
        {
            player = null;
        }
    }
}
