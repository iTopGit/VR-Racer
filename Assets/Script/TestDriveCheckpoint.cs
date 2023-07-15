using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDriveCheckpoint : MonoBehaviour
{

    private TestDriveManager testDriveManager;
    // Start is called before the first frame update
    void Start()
    {
        testDriveManager = GameObject.Find("Test Drive Manager").GetComponent<TestDriveManager>() ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Pass");
            testDriveManager.ThroughCheckpoint();
            gameObject.SetActive(false);
        }
    }
}
