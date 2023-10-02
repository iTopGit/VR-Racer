using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimitZone : MonoBehaviour
{
    [SerializeField] private int zoneLimit = 0;
    DistanceTrackController gameTracker;
    private bool allow2Check = true;

    // Start is called before the first frame update
    void Start() { gameTracker = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>(); }

    private void OnTriggerEnter(Collider other) { if(other.CompareTag("Player") && allow2Check) { 
            gameTracker.setSpeedZone(zoneLimit);
            allow2Check = false;
        } }

    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) { StartCoroutine(allowAgain()); } }

    IEnumerator allowAgain() {
        yield return new WaitForSeconds(15f);
        allow2Check = true;
    }
}
