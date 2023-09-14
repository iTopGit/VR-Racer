using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPlayer : MonoBehaviour
{
    [SerializeField] private int tracker_id = 0;
    [SerializeField] private float cooldown_trigger = 15.0f;
    private bool allow2Check = true;
    
    public GameObject distanceController;
    DistanceTrackController script;

    // Start is called before the first frame update
    void Start()
    {
        if(distanceController != null) { 
            script = distanceController.GetComponent<DistanceTrackController>();
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && allow2Check && tracker_id == script.currTracker) {
            allow2Check = false;
            script.moveTracker();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(allowAgain());
        }
    }

    IEnumerator allowAgain()
    {
        yield return new WaitForSeconds(cooldown_trigger);
        allow2Check = true;
    }
}
