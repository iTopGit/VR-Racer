using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCircle : MonoBehaviour
{
    [SerializeField] private int circus_id = 0;

    private bool allowCount = true;
    DistanceTrackController gameController;
    CarController carController;

    void Start() { 
        gameController = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>();
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && allowCount) {
            allowCount = false;
            gameController.trafficCirclePassing(circus_id);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            gameController.trafficCirclePassing(0);
            StartCoroutine(wait_check_count());
        }
    }

    IEnumerator wait_check_count()
    {
        yield return new WaitForSeconds(75);
        allowCount = true;
    }


}
