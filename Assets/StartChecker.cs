using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    public GameObject distanceController;
    DistanceTrackController script;

    void Start()
    {
        script = distanceController.GetComponent<DistanceTrackController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && distanceController != null) {
            script.enableTracking();
        }
    }
}
