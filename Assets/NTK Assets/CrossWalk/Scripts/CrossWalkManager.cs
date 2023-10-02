using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWalkManager : MonoBehaviour
{
    [SerializeField] private int objectId = 0;
    public GameObject[] humanPrefabs;
    [SerializeField] public GameObject spawnPoint;

    DistanceTrackController gameTracker;
    CarController carController;
    GameObject newHuman;
    HumanController humanController;
    bool move = false;
    bool allowCheck = true;

    private void Start()
    {
        gameTracker = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>();
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }
    void spawnHuman()
    {
        int humanIndex = Random.Range(0, humanPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;
        newHuman = Instantiate(humanPrefabs[1], spawnPosition, spawnRotation);
        humanController = newHuman.GetComponent<HumanController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && allowCheck)
        {
            spawnHuman();
            gameTracker.CrosswalkPassing(objectId);
            allowCheck = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carController.currentSpeed < 5 && !move && (humanController != null) && other.gameObject.tag == "Player")
        {
            move = true;
            humanController.setSpeed(4);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!move)
            {
                move = true;
                humanController.setSpeed(4);
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
