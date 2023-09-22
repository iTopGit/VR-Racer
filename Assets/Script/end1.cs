using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class end1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            Debug.Log("Player StopRecord Record");
            //VideoCaptureCtrl.instance.StopCapture();
        }
    }
}
