using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class EndCam1 : MonoBehaviour
{
    // [SerializeField]
    public GameObject cameraPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")  // Use CompareTag for better performance
        {
            Debug.Log("Player Stop Record Cam1");
            VideoCaptureCtrl.instance.StopCapture();
        }
    }
}
