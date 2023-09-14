using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCircle : MonoBehaviour
{
    private bool allowCount = true;
    GameObject gameController;
    void Start() { gameController = GameObject.FindGameObjectWithTag("distanceTrack"); }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player" && allowCount) {
            allowCount = false;
            gameController.GetComponent<DistanceTrackController>().trafficCirclePassed();
            StartCoroutine(wait_check());
        }
    }

    IEnumerator wait_check()
    {
        yield return new WaitForSeconds(75);
        allowCount = true;
    }
}
