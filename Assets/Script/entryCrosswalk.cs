using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entryCrosswalk : MonoBehaviour
{
    [SerializeField] private int crosswalk_id = 0;

    private bool allowCount = true;
    DistanceTrackController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("distanceTrack").GetComponent<DistanceTrackController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && allowCount)
        {
            allowCount = false;
            gameController.entry_Crosswalk(crosswalk_id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameController.entry_Crosswalk(0);
            // StartCoroutine(wait_check_count());
            allowCount = true;
        }
    }

    IEnumerator wait_check_count()
    {
        yield return new WaitForSeconds(30);
        allowCount = true;
    }
}
