using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCircle : MonoBehaviour
{
    [SerializeField] private int circus_id = 0;

    private bool isPassing = true;
    private bool allowCount = true;
    DistanceTrackController gameController;
    CarController carController;

    void Start() { 
        gameController = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>();
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && carController.currentSpeed == 0)
        {
            isPassing = false;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && allowCount) {
            allowCount = false;
            if(isPassing) { gameController.trafficCirclePassing();  }
            // gameController.trafficCircleChecking();
            StartCoroutine(wait_check_count());
        }
    }

    IEnumerator wait_check_count()
    {
        yield return new WaitForSeconds(75);
        allowCount = true;
        isPassing = true;
    }


}
