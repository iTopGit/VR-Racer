using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWalkManager : MonoBehaviour
{
    [SerializeField] private int objectId = 0;
    public GameObject[] humanPrefabs;
    [SerializeField] public GameObject spawnPoint;

    DistanceTrackController gameTracker;
    GameObject newHuman;
    HumanController humanController;
    bool move = false;
    bool allowCheck = true;

    float carSpeed;

    private void Start()
    {
        gameTracker = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>();
    }
    void spawnHuman()
    {
        int humanIndex = Random.Range(0, humanPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;
        newHuman = Instantiate(humanPrefabs[1], spawnPosition, spawnRotation);
        humanController = newHuman.GetComponent<HumanController>();
    }

    void Update() { carSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().currentSpeed; }
    private void OnTriggerEnter(Collider other) { 
        if (other.gameObject.tag == "Player" && allowCheck) { 
            spawnHuman();
            gameTracker.CrosswalkPassing(objectId);
            allowCheck = false;
        } 
    }

    private void OnTriggerStay(Collider other) {
        if (carSpeed < 5 && !move && (humanController != null) && other.gameObject.tag == "Player") {
            Debug.Log("On Trigger Stay.");
            move = true;
            humanController.setSpeed(4f);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            if(!move)
            {
                Debug.Log("On Trigger Exit.");
                move = true;
                humanController.setSpeed(4f);
            }
            gameTracker.CrosswalkPassing(0);
            StartCoroutine(resetChecker());
        }
    }

    IEnumerator resetChecker()
    {
        yield return new WaitForSeconds(60);
        allowCheck = true;
        move = false;
    }
}
