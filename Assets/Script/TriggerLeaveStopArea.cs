using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeaveStopArea : MonoBehaviour
{

    private TestDriveStopArea testDriveStopArea;
    // Start is called before the first frame update
    void Start()
    {
        testDriveStopArea = gameObject.transform.parent.gameObject.GetComponentInChildren<TestDriveStopArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            testDriveStopArea.LeaveArea();
        }
    }

}
